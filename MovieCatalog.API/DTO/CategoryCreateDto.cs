using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.API.DTO
{
    public class CategoryCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
