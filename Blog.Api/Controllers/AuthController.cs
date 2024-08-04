using Blog.Api.Models;
using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using BlogWeb.MVCUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;



namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IManager<User> _userManager;
        private readonly TokenService _tokenService;

        public AuthController(IManager<User> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO model)
        {


            var user = await _userManager.ValidateUserAsync(model.UserName, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user);
            var userResponse = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role
            };
            return Ok(new { Token = token, User = userResponse });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Name = model.Name,
                LastName = model.LastName,
                Mail = model.Mail,
                Password = model.Password,
                ProfilePhotoUrl = model.ProfilePhotoUrl,

            };

            await _userManager.AddAsync(user);

            return Ok(new { Message = "User registered successfully" });
        }
    }
}
