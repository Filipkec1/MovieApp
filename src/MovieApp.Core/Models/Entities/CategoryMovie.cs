using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Core.Models.Entities
{
    public class CategoryMovie : BaseModel
    {
        public Movie Movie { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Movie))]
        public Guid MovieId { get; set; }

        public Category Category { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
    }
}