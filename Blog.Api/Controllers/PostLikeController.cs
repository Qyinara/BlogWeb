using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostLikeController : ControllerBase
    {
        private readonly IManager<PostLike> _postLikeManager;

        public PostLikeController(IManager<PostLike> postLikeManager)
        {
            _postLikeManager = postLikeManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostLike>>> GetAll()
        {
            var postLikes = await Task.FromResult(_postLikeManager.GetAll());
            return Ok(postLikes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostLike>> GetById(int id)
        {
            var postLike = await Task.FromResult(_postLikeManager.GetById(id));
            if (postLike == null)
            {
                return NotFound();
            }
            return Ok(postLike);
        }

        [HttpPost]
        public async Task<ActionResult<PostLike>> Create(PostLike postLike)
        {
            await Task.Run(() => _postLikeManager.Insert(postLike));
            return CreatedAtAction(nameof(GetById), new { id = postLike.Id }, postLike);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PostLike postLike)
        {
            if (id != postLike.Id)
            {
                return BadRequest();
            }

            await Task.Run(() => _postLikeManager.Update(postLike));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => _postLikeManager.DeleteById(id));
            return NoContent();
        }
    }
}
