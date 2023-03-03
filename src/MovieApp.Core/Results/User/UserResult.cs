using MovieApp.Core.Models.Entities;

namespace MovieApp.Core.Results
{
    /// <summary>
    /// Defines <see cref=" User"/> result class.
    /// </summary>
    public class UserResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UserResult"/>.
        /// </summary>
        /// <param name="user"><see cref="User"/></param>
        public UserResult(User user)
        {
            Id= user.Id;
            Name= user.Name;
            Role = user.Role.Name.ToString();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}