using Blog.Api.Models;
using Blog.BL.Managers.Abstract;
using Blog.BL.Managers.Concrete;
using Blog.Entities.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IManager<Comment> _commentManager;
        private readonly IManager<Post> _postManager;
        private readonly IManager<User> _userManager;
        public CommentController(IManager<Comment> commentManager, IManager<Post> postManager, IManager<User> userManager)
        {
            _commentManager = commentManager;
            _postManager = postManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAll()
        {
            var comments = await Task.FromResult(_commentManager.GetAll());
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetById(int id)
        {
            var comment = await Task.FromResult(_commentManager.GetById(id));
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Create(CommentDto commentDto)
        {
            var post = _postManager.GetById(commentDto.PostId);
            var author = _userManager.GetById(commentDto.AuthorId);

            if (post == null || author == null)
            {
                return BadRequest("Invalid post or author ID.");
            }

            var comment = new Comment
            {
                PostId = commentDto.PostId,
                AuthorId = commentDto.AuthorId,
                Content = commentDto.Content,
                CommentImageURL = commentDto.CommentImageURL,
                CreateDate = DateTime.Now,
                Post = post,
                Author = author
            };

            await _commentManager.AddAsync(comment);

            // Yorum başarıyla eklendikten sonra sadece basit bir yanıt döndürüyoruz
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, new { Message = "Comment created successfully." });
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            await Task.Run(() => _commentManager.Update(comment));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => _commentManager.DeleteById(id));
            return NoContent();
        }
    }
}
