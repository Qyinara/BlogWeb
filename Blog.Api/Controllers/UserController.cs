using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IManager<User> _userManager;

        public UserController(IManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await Task.FromResult(_userManager.GetAll());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userManager.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _userManager.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = user.UserName;
            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.Mail = user.Mail;
            existingUser.Password = user.Password;
            existingUser.ProfilePhotoUrl = user.ProfilePhotoUrl;
            existingUser.RoleId = user.RoleId;

            await _userManager.UpdateAsync(existingUser);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await _userManager.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _userManager.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userManager.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
