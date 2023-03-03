using MovieApp.Core.Models.Entities;

namespace MovieApp.Core.Request
{
    /// <summary>
    /// Defines <see cref="Category"/> create request.
    /// </summary>
    public class CategoryCreateRequest
    {
        public string Name { get; set; }
    }
}