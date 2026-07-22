using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MovieCatalog.API.Models
{
    public class AppDbContext :IdentityDbContext<ApplicationUser>
    {
         public AppDbContext(DbContextOptions<AppDbContext > options) 
            : base(options) 
        { 
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }




    }
}
