using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.API.DTO;
using MovieCatalog.API.Models;

namespace MovieCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesControllerDto : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesControllerDto(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var movies = _context.Movies
                .Include(m => m.Category)
                .Select(m => new MovieReadDto
                {
                    Id = m.id,
                    Title=m.Title,
                    Rating = m.Rating,
                    CategoryName=m.Category.Name

                })
                .ToList();

            return Ok(movies);
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Category)
                .Where(m => m.id == id)
                .Select(m => new MovieReadDto
                {
                    Id = m.id,
                    Title = m.Title,
                    Rating = m.Rating,
                    CategoryName = m.Category.Name

                }).
                FirstOrDefault();

            if(movie==null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]

        public IActionResult create(MovieCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryExists = _context.Categories
                .Any(c => c.Id == dto.CategoryId);

            if (!categoryExists)
            {
                return NotFound("Category not found");
            }

            var movie = new Movie
            {
                Title = dto.Title,
                ReleaseYeard = dto.ReleaseYear,
                Rating = dto.Rating,
                Director = dto.Director,
                CategoryId = dto.CategoryId

            };

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get),  new { Id = movie.id }, null);

        }

        [HttpPut("{id}")]

        public IActionResult Updatee(int id, MovieUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _context.Movies.Find(id);

            if (movie == null)
                return NotFound();

            movie.Title = dto.Title;
            movie.ReleaseYeard = dto.ReleaseYear;
            movie.Rating = dto.Rating;
            movie.Director = dto.Director;
            movie.CategoryId = dto.CategoryId;

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
