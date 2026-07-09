using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.API.DTO
{
    public class MovieCreateDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Range(1950, 2035)]
        public int ReleaseYear { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public string Director { get; set; }

        public int CategoryId { get; set; }
    }
}
