using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.API.Models;

namespace MovieCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories;
            return Ok(categories);
        }
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var Category = _context.Categories.Find(id);

            if (Category == null)
                return NotFound();

            return Ok(Category);

        }

        [HttpPost]

        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpPut("{id}")]

        public IActionResult Updatee(int id,Category category)
        {
            if(id!=category.Id)
            {
                return BadRequest();
            }

            _context.Categories.Update(category);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]

       public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if(category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return NoContent();


        }
           

    }
}
