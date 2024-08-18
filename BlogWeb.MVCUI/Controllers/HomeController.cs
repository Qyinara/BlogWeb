using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using Blog.Entities.DbContexts; // AppDbContext'in bulunduðu namespace'i ekleyin
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; // Authorize attribute'u için gerekli
using System.Security.Claims;

namespace BlogWeb.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IManager<Post> _postManager;
        private readonly AppDbContext _context; // AppDbContext'i kullanarak Include metodunu kullanabilmek için

        public HomeController(IManager<Post> postManager, AppDbContext context)
        {
            _postManager = postManager;
            _context = context;
        }

        public IActionResult Index()
        {
            // Posts verilerini Author, Category, Comments ve PostLikes bilgileriyle birlikte al
            var posts = _context.Posts
                                .Include(p => p.Author)
                                .Include(p => p.Category)
                                .Include(p => p.Comments)
                                .Include(p => p.PostLikes)
                                .ToList();

            return View(posts);
        }

        public IActionResult PostDetails(int id)
        {
            var post = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                .Include(p => p.PostLikes)
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var viewModel = new PostDetailsViewModel
            {
                Post = post,
                Comments = post.Comments.ToList(),
                TotalLikes = post.PostLikes.Count
            };

            return View(viewModel);
        }





        [HttpPost]
        public IActionResult AddComment(int PostId, string Content)
        {
            // Kullanýcý giriþ yaptýysa, kullanýcýnýn ID'sini almanýz gerekiyor. 
            // Örneðin, eðer Identity kullanýyorsanýz:
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); // Kullanýcý giriþ yapmamýþsa, giriþ sayfasýna yönlendirin.
            }

            // Yeni bir yorum oluþturuyoruz.
            var comment = new Comment
            {
                PostId = PostId,
                Content = Content,
                AuthorId = int.Parse(userId), // Kullanýcý ID'si int tipinde olmalý, gerekirse dönüþtürün
                CreateDate = DateTime.Now
            };

            // Veritabanýna ekliyoruz.
            _context.Comments.Add(comment);
            _context.SaveChanges();

            // Yorum eklendikten sonra tekrar PostDetails sayfasýna yönlendiriyoruz.
            return RedirectToAction("PostDetails", new { id = PostId });
        }







    }
}
