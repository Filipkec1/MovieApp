using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Core.Models.Entities
{
    public class Category : BaseModel
    {
        public Category()
        {
            CategoryMovie = new HashSet<CategoryMovie>();
        }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        public virtual ICollection<CategoryMovie> CategoryMovie { get; set; }
    }
}