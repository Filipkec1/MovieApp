using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Core.Models.Entities
{
    public class User : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string Hash { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        public Role Role { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }
    }
}