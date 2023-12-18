using nov30task.Models;

namespace nov30task.ViewModels.BlogsVM
{
    public class BlogListItemVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
