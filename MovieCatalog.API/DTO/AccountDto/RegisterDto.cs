using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.API.DTO.AccountDto
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Hobby { get; set; }


    }
}
