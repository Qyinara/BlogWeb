using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using BlogWeb.MVCUI.Models;

namespace BlogWeb.MVCUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IManager<User> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(IManager<User> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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
        public async Task<IActionResult> Register(UserDTO model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Name = model.Name,
                LastName = model.LastName,
                Mail = model.Mail,
                Password = model.Password,
                RoleId = 2, // Varsayılan olarak atadığım user rolü
                Role = "User",
                Rolee = null
            };

            try
            {
                await _userManager.AddAsync(user);
                ViewBag.SuccessMessage = "Registration successful! You can now login.";
                return RedirectToAction("Login");
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
