using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using BlogWeb.MVCUI.Models;
using Microsoft.AspNetCore.Hosting;

namespace BlogWeb.MVCUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IManager<User> _userManager;
        private readonly TokenService _tokenService;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountController(IManager<User> userManager, TokenService tokenService, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTO model)
        {
            var user = await _userManager.ValidateUserAsync(model.UserName, model.Password);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid login attempt";
                return View(model);
            }

            // Kullanıcıya ait claims oluşturuluyor
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Dosya yükleme işlemi
            string uniqueFileName = "default.png"; // Varsayılan dosya adı

            if (model.ProfilePhoto != null && model.ProfilePhoto.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profilephoto");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfilePhoto.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePhoto.CopyToAsync(fileStream);
                }
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    LastName = model.LastName,
                    Mail = model.Mail,
                    Password = model.Password,
                    RoleId = 2, // Varsayılan olarak "User" rolü, ID'si 2
                    Role = "User",
                    ProfilePhotoUrl = uniqueFileName // Sadece dosya adını kaydediyoruz
                };

                try
                {
                    await _userManager.AddAsync(user);

                    // Kayıt başarılı olursa login sayfasına yönlendirin
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Kayıt sırasında bir hata oluştu: " + ex.Message;
                }
            }

            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}