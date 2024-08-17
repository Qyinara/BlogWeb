using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blog.Entities.Models.Concrete;
using BlogWeb.MVCUI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blog.Entities.DbContexts;

namespace BlogWeb.MVCUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public AdminController(IHttpClientFactory httpClientFactory, AppDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>("api/User");

            var userViewModels = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Mail = user.Mail,
                ProfilePhotoUrl = user.ProfilePhotoUrl,
                Role = user.RoleId == 1 ? "Admin" : "User"
            }).ToList();

            return View(userViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"api/User/{id}");
            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Mail = user.Mail,
                ProfilePhotoUrl = user.ProfilePhotoUrl,
                Role = user.RoleId == 1 ? "Admin" : "User"
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _httpClient.GetFromJsonAsync<User>($"api/User/{userViewModel.Id}");
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = userViewModel.UserName;
                user.Mail = userViewModel.Mail;
                user.ProfilePhotoUrl = userViewModel.ProfilePhotoUrl;
                user.RoleId = userViewModel.Role == "Admin" ? 1 : 2;

                var response = await _httpClient.PutAsJsonAsync($"api/User/{user.Id}", user);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Users");
                }

                ModelState.AddModelError("", "Error updating user.");
            }

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Users");
            }

            ModelState.AddModelError("", $"Error deleting user with ID {id}.");
            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var existingCategories = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");
                if (existingCategories.Any(c => c.CategoryName == category.CategoryName))
                {
                    ModelState.AddModelError("CategoryName", "Aynı isimden birden fazla kategori olamaz.");
                    return View(category);
                }

                var response = await _httpClient.PostAsJsonAsync("api/Category", category);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Categories");
                }

                ModelState.AddModelError("", "Error adding category.");
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Category/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Categories");
            }

            ModelState.AddModelError("", "Error deleting category.");
            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> Posts()
        {
            var posts = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .ToListAsync();

            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> AddPost()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");

            // Giriş yapan kullanıcıyı al
            var userName = User.Identity.Name;
            var authors = await _httpClient.GetFromJsonAsync<List<User>>("api/User");
            var currentUser = authors.FirstOrDefault(u => u.UserName == userName);

            ViewBag.Categories = categories;
            ViewBag.Authors = authors;
            ViewBag.AuthorId = currentUser?.Id; // AuthorId'yi ViewBag ile View'e gönder

            return View(new PostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromForm] PostViewModel post)
        {
            if (post == null)
            {
                return BadRequest("Post data is null.");
            }

            if (!ModelState.IsValid)
            {
                // Hataları tekrar kullanıcıya göster
                var categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");
                var authors = await _httpClient.GetFromJsonAsync<List<User>>("api/User");

                ViewBag.Categories = categories;
                ViewBag.Authors = authors;

                return View(post);
            }

            // Giriş yapan kullanıcının AuthorId olarak atanması
            var userName = User.Identity.Name;
            var currentUser = (await _httpClient.GetFromJsonAsync<List<User>>("api/User")).FirstOrDefault(u => u.UserName == userName);
            if (currentUser != null)
            {
                post.AuthorId = currentUser.Id;
            }

            // Foreign key'lerin doğru atanıp atanmadığını kontrol edin
            if (post.AuthorId == 0 || post.CategoryId == 0)
            {
                ModelState.AddModelError("", "AuthorId and CategoryId must be provided.");
                var categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");
                var authors = await _httpClient.GetFromJsonAsync<List<User>>("api/User");

                ViewBag.Categories = categories;
                ViewBag.Authors = authors;

                return View(post);
            }

            var response = await _httpClient.PostAsJsonAsync("api/Post", post);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Posts",post);
            }

            var categorieses = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");
            var authorses = await _httpClient.GetFromJsonAsync<List<User>>("api/User");

            ViewBag.Categories = categorieses;
            ViewBag.Authors = authorses;

            ModelState.AddModelError("", "Error adding post.");
            return View(post);
        }


        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Post/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Posts");
            }

            ModelState.AddModelError("", "Error deleting post.");
            return RedirectToAction("Posts");
        }
    }
}
