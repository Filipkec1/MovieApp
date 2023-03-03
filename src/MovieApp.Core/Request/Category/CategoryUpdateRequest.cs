using MovieApp.Core.Models.Entities;

namespace MovieApp.Core.Request
{
    /// <summary>
    /// Defines <see cref="Category"/> update request.
    /// </summary>
    public class CategoryUpdateRequest
    {
        public string Name { get; set; }
    }
}