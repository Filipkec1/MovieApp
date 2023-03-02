using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Core.Models.Entities
{
    public class User : BaseModel
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }
    }
}