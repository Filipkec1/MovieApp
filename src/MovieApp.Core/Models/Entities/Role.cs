namespace MovieApp.Core.Models.Entities
{
    public class Role : BaseModel
    {
        public Role() 
        {
            User = new HashSet<User>();
        }

        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}