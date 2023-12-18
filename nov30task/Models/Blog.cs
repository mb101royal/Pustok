using System.ComponentModel.DataAnnotations;

namespace nov30task.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string? Title { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        public ICollection<BlogTag>? BlogTags { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
