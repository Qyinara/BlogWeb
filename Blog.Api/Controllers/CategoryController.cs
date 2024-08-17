using Blog.BL.Managers.Abstract;
using Blog.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IManager<Category> _categoryManager;

        public CategoryController(IManager<Category> categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await Task.FromResult(_categoryManager.GetAll());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await Task.FromResult(_categoryManager.GetById(id));
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            await Task.Run(() => _categoryManager.Insert(category));
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            await Task.Run(() => _categoryManager.Update(category));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => _categoryManager.DeleteById(id));
            return NoContent();
        }
    }
}