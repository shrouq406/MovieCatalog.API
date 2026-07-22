using Microsoft.AspNetCore.Identity;

namespace MovieCatalog.API.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Hobby{ get; set; }

    }
}
