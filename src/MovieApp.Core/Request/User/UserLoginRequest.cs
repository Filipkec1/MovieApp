using MovieApp.Core.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Core.Request
{
    /// <summary>
    /// Defines <see cref="User"/> login request.
    /// </summary>
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}