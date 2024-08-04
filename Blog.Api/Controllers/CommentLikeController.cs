using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentLikeController : ControllerBase
    {
        private readonly IManager<CommentLike> _commentLikeManager;

        public CommentLikeController(IManager<CommentLike> commentLikeManager)
        {
            _commentLikeManager = commentLikeManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentLike>>> GetAll()
        {
            var commentLikes = await Task.FromResult(_commentLikeManager.GetAll());
            return Ok(commentLikes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentLike>> GetById(int id)
        {
            var commentLike = await Task.FromResult(_commentLikeManager.GetById(id));
            if (commentLike == null)
            {
                return NotFound();
            }
            return Ok(commentLike);
        }

        [HttpPost]
        public async Task<ActionResult<CommentLike>> Create(CommentLike commentLike)
        {
            await Task.Run(() => _commentLikeManager.Insert(commentLike));
            return CreatedAtAction(nameof(GetById), new { id = commentLike.Id }, commentLike);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CommentLike commentLike)
        {
            if (id != commentLike.Id)
            {
                return BadRequest();
            }

            await Task.Run(() => _commentLikeManager.Update(commentLike));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => _commentLikeManager.DeleteById(id));
            return NoContent();
        }
    }
}
