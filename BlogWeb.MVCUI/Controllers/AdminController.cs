using Blog.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlogWeb.MVCUI.Models;
using Microsoft.EntityFrameworkCore;
using Blog.Entities.DbContexts;
using X.PagedList.Extensions;

namespace BlogWeb.MVCUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IHttpClientFactory httpClientFactory, AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users(int? page)
        {
            // Sayfa başına gösterilecek öğe sayısı
            int pageSize = 10;
            // Sayfa numarası, eğer null ise 1. sayfa olarak ayarla
            int pageNumber = page ?? 1;

            // API'den kullanıcı verilerini çek
            var users = await _httpClient.GetFromJsonAsync<List<User>>("api/User");

            // Kullanıcıları ViewModel'e dönüştür
            var userViewModels = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Mail = user.Mail,
                ProfilePhotoUrl = user.ProfilePhotoUrl,
                Role = user.RoleId == 1 ? "Admin" : "User"
            }).ToList();

            // Kullanıcı verilerini sayfalara böl
            var pagedUsers = userViewModels.ToPagedList(pageNumber, pageSize);

            // PagedUserViewModel'e atama yap
            var viewModel = new PagedUserViewModel
            {
                Users = pagedUsers
            };

            return View(viewModel);
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


        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _httpClient.GetFromJsonAsync<Category>($"api/Category/{id}");
            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = model.Id,
                    CategoryName = model.CategoryName
                };

                var response = await _httpClient.PutAsJsonAsync($"api/Category/{category.Id}", category);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Categories");
                }

                ModelState.AddModelError("", "Error updating category.");
            }

            return View(model);
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

 
            }

            // Dosya yükleme işlemi
            if (post.ImageFile != null && post.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "posts");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(post.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await post.ImageFile.CopyToAsync(fileStream);
                }

                post.PostImageURL = "/images/posts/" + uniqueFileName; // PostImageURL alanına dosya yolunu ekliyoruz
            }

            // API'ye post verilerini gönderme
            var response = await _httpClient.PostAsJsonAsync("api/Post", post);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Posts");
            }

            var categorieses = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");
            var authorses = await _httpClient.GetFromJsonAsync<List<User>>("api/User");

            ViewBag.Categories = categorieses;
            ViewBag.Authors = authorses;

            ModelState.AddModelError("", "Error adding post.");
            return View(post);
        }

    




    [HttpGet]
        public async Task<IActionResult> UpdatePost(int id)
        {
            var post = await _httpClient.GetFromJsonAsync<Post>($"api/Post/{id}");
            if (post == null)
            {
                return NotFound();
            }

            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CategoryId = post.CategoryId,
                AuthorId = post.AuthorId,
                PostImageURL = post.PostImageURL
            };

            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");
            ViewBag.Categories = categories.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList();

            var authors = await _httpClient.GetFromJsonAsync<List<User>>("api/User");
            ViewBag.Authors = authors.Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.UserName
            }).ToList();

            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = await _httpClient.GetFromJsonAsync<Post>($"api/Post/{model.Id}");
                if (post == null)
                {
                    return NotFound();
                }
                post.Id = model.Id;
                post.Title = model.Title;
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                post.AuthorId = model.AuthorId;

                if (model.ImageFile != null)
                {
                    var fileName = Path.GetFileName(model.ImageFile.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "posts", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    post.PostImageURL = "/images/posts/" + fileName;
                }

                var response = await _httpClient.PutAsJsonAsync($"api/Post/{post.Id}", post);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Posts");
                }

                ModelState.AddModelError("", "Error updating post.");
            }

            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/Category");
            ViewBag.Categories = categories.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList();

            var authors = await _httpClient.GetFromJsonAsync<List<User>>("api/User");
            ViewBag.Authors = authors.Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.UserName
            }).ToList();

            return View(model);
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
