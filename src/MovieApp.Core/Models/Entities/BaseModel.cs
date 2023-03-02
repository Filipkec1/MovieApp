using System.ComponentModel.DataAnnotations;

namespace MovieApp.Core.Models.Entities
{
    /// <summary>
    /// Base model for all other models
    /// </summary>
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}