using Blog.Entities.Models.Concrete;
using Blog.Entities.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogWeb.MVCUI.Models;
using X.PagedList;
using X.Web.PagedList;
using X.PagedList.Extensions;

namespace BlogWeb.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string titleFilter = null, int? categoryFilter = null, int page = 1, int pageSize = 4)
        {
         
            var postsQuery = _context.Posts
                                     .Include(p => p.PostLikes)
                                     .Include(p => p.Comments)
                                     .Include(p => p.Author)
                                     .Include(p => p.Category)
                                     .OrderByDescending(p => p.CreateDate)
                                     .AsQueryable();

          
            if (categoryFilter.HasValue && categoryFilter.Value > 0)
            {
                postsQuery = postsQuery.Where(p => p.CategoryId == categoryFilter.Value);
            }

            var posts = await postsQuery.ToListAsync();

          
            if (!string.IsNullOrEmpty(titleFilter))
            {
                posts = posts.Where(p => p.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

      
            var postViewModels = posts.Select(post => new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                Author = post.Author,
                CategoryId = post.CategoryId,
                Category = post.Category,
                PostImageURL = post.PostImageURL,
                Comments = post.Comments,
                PostLikes = post.PostLikes
            }).ToList();

         
            var pagedList = postViewModels.ToPagedList(page, pageSize);

     
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.TitleFilter = titleFilter;
            ViewBag.CategoryFilter = categoryFilter;

            return View(pagedList);
        }






        public async Task<IActionResult> PostDetails(int id, int page = 1, int pageSize = 5)
        {
            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                .Include(p => p.PostLikes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {   
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var hasLiked = post.PostLikes.Any(pl => pl.UserId == userId);

            var pagedComments = post.Comments.OrderByDescending(c => c.CreateDate)
                                              .ToPagedList(page, pageSize);

            var viewModel = new PostDetailsViewModel
            {
                Post = post,
                PagedComments = pagedComments,
                TotalLikes = post.PostLikes.Count,
                HasLiked = hasLiked,
                CommentLikes = await _context.CommentLikes.ToListAsync()
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> AddComment(int PostId, string Content)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                TempData["Error"] = "You must be logged in to comment.";
                return RedirectToAction("PostDetails", new { id = PostId });
            }

            var post = await _context.Posts.FindAsync(PostId);
            if (post == null)
            {
                TempData["Error"] = "Post not found.";
                return RedirectToAction("PostDetails", new { id = PostId });
            }

            var comment = new Comment
            {
                PostId = PostId,
                Content = Content,
                AuthorId = int.Parse(userId),
                CreateDate = DateTime.Now
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("PostDetails", new { id = PostId });
        }



        [HttpPost]
        public async Task<IActionResult> LikeComment(int commentId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

            if (userId == 0)
            {
                return Unauthorized();
            }

            var existingLike = await _context.CommentLikes
                .FirstOrDefaultAsync(cl => cl.CommentId == commentId && cl.UserId == userId);

            var comment = await _context.Comments
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (existingLike != null)
            {
                _context.CommentLikes.Remove(existingLike);

           
                var activity = await _context.Activities
                    .FirstOrDefaultAsync(a => a.UserId == userId
                                              && a.ActivityType == "Comment Like"
                                              && a.Description.Contains($"liked a comment on the post '{comment.Post.Title}'")
                                              && a.Description.Contains($"with content: '{comment.Content}'"));
                if (activity != null)
                {
                    _context.Activities.Remove(activity);
                }
            }
            else
            {
                var commentLike = new CommentLike
                {
                    CommentId = commentId,
                    UserId = userId
                };
                _context.CommentLikes.Add(commentLike);

                var activity = new Activity
                {
                    UserId = userId,
                    ActivityType = "Comment Like",
                    Description = $"You liked a comment on the post '{comment.Post.Title}' with content: '{comment.Content}'",
                    ActivityDate = DateTime.Now
                };
                _context.Activities.Add(activity);
            }

            await _context.SaveChangesAsync();

            var hasLikedComment = comment.CommentLikes.Any(cl => cl.UserId == userId);

            return Json(new { totalCommentLikes = comment.CommentLikes.Count, hasLikedComment });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.Comments
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var isAdmin = User.IsInRole("Admin");

            if (comment.AuthorId != userId && !isAdmin)
            {
                return Forbid(); 
            }

          
            var relatedActivities = await _context.Activities
                .Where(a => a.Description.Contains($"commented on the post '{comment.Post.Title}'") ||
                            a.Description.Contains($"liked a comment on the post '{comment.Post.Title}'"))
                .ToListAsync();

            _context.Activities.RemoveRange(relatedActivities);

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("PostDetails", new { id = comment.PostId });
        }


        [HttpPost]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _context.Posts.FindAsync(postId);
            if (userId == null)
            {
                return Unauthorized(); 
            }

            var userIntId = int.Parse(userId);

            var existingLike = await _context.PostLikes
                .FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userIntId);

            if (existingLike != null)
            {
                _context.PostLikes.Remove(existingLike);

           
                var activity = await _context.Activities
                    .FirstOrDefaultAsync(a => a.UserId == userIntId && a.ActivityType == "Post Like" && a.Description.Contains($"liked the post '{post.Title}'"));
                if (activity != null)
                {
                    _context.Activities.Remove(activity);
                }
            }
            else
            {
                var postLike = new PostLike
                {
                    PostId = postId,
                    UserId = userIntId,
                    CreateDate = DateTime.Now
                };

                _context.PostLikes.Add(postLike);

                var activity = new Activity
                {
                    UserId = userIntId,
                    ActivityType = "Post Like",
                    Description = $"You liked the post '{post.Title}'",
                    ActivityDate = DateTime.Now
                };
                _context.Activities.Add(activity);
            }

            await _context.SaveChangesAsync();

            post = await _context.Posts.Include(p => p.PostLikes).FirstOrDefaultAsync(p => p.Id == postId);
            var hasLiked = post.PostLikes.Any(pl => pl.UserId == userIntId);

            return Json(new { totalLikes = post.PostLikes.Count, hasLiked });
        }





        [HttpGet]
        public IActionResult Profile(int userId, int page = 1, int pageSize = 5)
        {
            var user = _context.Users
                .Include(u => u.Activities)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var activities = _context.Activities
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.ActivityDate)
                .ToPagedList(page, pageSize);

            var viewModel = new ProfileViewModel
            {
                User = user,
                Activities = activities
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index");
            }

            var users = _context.Users
                .Where(u => u.UserName.Contains(query) || u.Mail.Contains(query))
                .ToList();

            if (users.Count == 1)
            {
               
                return RedirectToAction("Profile", new { userId = users.First().Id });
            }

            return View("SearchResults", users);
        }



    }






}
