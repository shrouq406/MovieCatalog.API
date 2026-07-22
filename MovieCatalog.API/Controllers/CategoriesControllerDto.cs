using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.API.DTO;
using MovieCatalog.API.DTO.CategoryDto;
using MovieCatalog.API.Models;

namespace MovieCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesControllerDto : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesControllerDto(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var categories = _context.Categories
                .Select(c => new CategoryReadDto
                {
                    Id = c.Id,
                    Name = c.Name

                })
                .ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]

         public IActionResult GetById(int id)
        {
            var category = _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryReadDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefault();

            if(category ==null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]

        public IActionResult create(CategoryCreateDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new Category
            {
                Name=dto.Name
            };

            _context .Categories.Add(category);
            _context .SaveChanges();

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                new CategoryReadDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
        }

        [HttpDelete("{id}")]
        
        public IActionResult DeleteById(int id)
        {
            var category = _context.Categories.Find(id);

            if(category == null)
            {
                return NotFound();
            }

            _context.Categories .Remove(category);
            _context.SaveChanges();

            return NoContent();
        }


    }
}
