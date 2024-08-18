using Blog.Entities.Models.Concrete;
using Blog.Entities.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogWeb.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                                .Include(p => p.Author)
                                .Include(p => p.Category)
                                .Include(p => p.Comments)
                                .Include(p => p.PostLikes)
                                .ToListAsync();

            return View(posts);
        }

        public async Task<IActionResult> PostDetails(int id)
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

            var viewModel = new PostDetailsViewModel
            {
                Post = post,
                Comments = post.Comments.ToList(),
                TotalLikes = post.PostLikes.Count,
                HasLiked = hasLiked,
                CommentLikes = await _context.CommentLikes.ToListAsync() // CommentLikes'i de dahil ettik
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int PostId, string Content)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
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

            if (existingLike != null)
            {
                _context.CommentLikes.Remove(existingLike);
            }
            else
            {
                var commentLike = new CommentLike
                {
                    CommentId = commentId,
                    UserId = userId
                };

                _context.CommentLikes.Add(commentLike);
            }

            await _context.SaveChangesAsync();

            var comment = await _context.Comments.Include(c => c.CommentLikes).FirstOrDefaultAsync(c => c.Id == commentId);
            var hasLikedComment = comment.CommentLikes.Any(cl => cl.UserId == userId);

            return Json(new { totalCommentLikes = comment.CommentLikes.Count, hasLikedComment });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var isAdmin = User.IsInRole("Admin");

            if (comment.AuthorId != userId && !isAdmin)
            {
                return Forbid(); // Kullanýcý ne admin ne de yorumun sahibi deðilse, eriþim engellenir.
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("PostDetails", new { id = comment.PostId });
        }

        [HttpPost]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(); // Eðer kullanýcý giriþ yapmamýþsa, yetkisiz eriþim döndür.
            }

            var userIntId = int.Parse(userId);

            var existingLike = await _context.PostLikes
                .FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userIntId);

            if (existingLike != null)
            {
                _context.PostLikes.Remove(existingLike);
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
            }

            await _context.SaveChangesAsync();

            var post = await _context.Posts.Include(p => p.PostLikes).FirstOrDefaultAsync(p => p.Id == postId);
            var hasLiked = post.PostLikes.Any(pl => pl.UserId == userIntId);

            return Json(new { totalLikes = post.PostLikes.Count, hasLiked });
        }

    }
}
