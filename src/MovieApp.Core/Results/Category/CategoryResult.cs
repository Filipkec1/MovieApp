using MovieApp.Core.Models.Entities;

namespace MovieApp.Core.Results
{
    /// <summary>
    /// Defines <see cref="Category"/> result class.
    /// </summary>
    public class CategoryResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MovieResult"/>.
        /// </summary>
        /// <param name="category"><see cref="Category"/></param>
        public CategoryResult(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}