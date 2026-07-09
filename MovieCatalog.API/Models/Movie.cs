using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCatalog.API.Models
{
    public class Movie
    {
        public int id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Range(1950,2035)]
        public int ReleaseYeard { get; set; }

        [Range(1,10)]
        public int Rating { get; set; }

        [Required ]
        public string Director { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
