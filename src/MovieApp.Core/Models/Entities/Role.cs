using MovieApp.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Core.Models.Entities
{
    public class Role : BaseModel
    {
        public Role() 
        {
            User = new HashSet<User>();
        }

        [Required]
        public RoleEnum Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}