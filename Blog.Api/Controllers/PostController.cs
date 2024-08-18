using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using BlogWeb.MVCUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IManager<Post> _postManager;
        private readonly IManager<User> _userManager;
        private readonly IManager<Category> _categoryManager;

        public PostController(IManager<Post> postManager, IManager<User> userManager, IManager<Category> categoryManager)
        {
            _postManager = postManager;
            _userManager = userManager;
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll()
        {
            var posts = await Task.FromResult(_postManager.GetAll());
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetById(int id)
        {
            var post = await Task.FromResult(_postManager.GetById(id));
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Create(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetById(post.AuthorId);
                var category = _categoryManager.GetById(post.CategoryId);

                Post newPost = new Post()
                {
                    AuthorId = post.AuthorId,
                    CategoryId = post.CategoryId,
                    Content = post.Content,
                    Title = post.Title,
                    Comments = post.Comments,
                    PostImageURL = post.PostImageURL,
                    CreateDate = DateTime.Now,
                    PostLikes = post.PostLikes,
                    Author=user,
                    Category=category
                };
                await _postManager.AddAsync(newPost);
                //return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PostViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var post = await Task.FromResult(_postManager.GetById(id));
            if (post == null)
            {
                return NotFound();
            }

            // Mevcut Author ve Category bilgilerini alıyoruz
            var author = _userManager.GetById(model.AuthorId);
            var category = _categoryManager.GetById(model.CategoryId);

            post.Title = model.Title;
            post.Content = model.Content;
            post.CategoryId = model.CategoryId;
            post.AuthorId = model.AuthorId;
            post.PostImageURL = model.PostImageURL;
            post.Author = author;
            post.Category = category;

            await Task.Run(() => _postManager.Update(post));

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => _postManager.DeleteById(id));
            return NoContent();
        }
    }
}
