using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.API.Models;

namespace MovieCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]

    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var movies = _context.Movies.Include(m => m.Category).ToList();

            return Ok(movies);

        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var movies = _context.Movies.Include(_m => _m.Category)
                .ToList()
                .FirstOrDefault(m => m.id == id);

            if( movies == null )
            {
                return NotFound();
            }

            return Ok(movies);

        }


        [HttpPost]

        public IActionResult creat(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return Ok(movie);
              
        }

        [HttpPut("{id}")]

        public IActionResult Updatee(int id,Movie movie)
        {
            if(id != movie.id)
            {
                return BadRequest();
            }

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);

            if(movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }


    }
}
