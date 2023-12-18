using System.ComponentModel.DataAnnotations;

namespace nov30task.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required, MaxLength(32)]
        public string? Name { get; set; }
        [MaxLength(42)]
        public string? Surname { get; set; }
        public List<Blog>? Blogs { get; set; }
    }
}
