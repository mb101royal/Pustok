using nov30task.Models;
using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.BlogsVM
{
    public class BlogUpdateVM
    {
        [Required, MaxLength(128)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
