using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.API.DTO.CategoryDto
{
    public class CategoryCreateDto //Data transfer object
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
