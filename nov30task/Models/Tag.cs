using System.ComponentModel.DataAnnotations;

namespace nov30task.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public ICollection<Blog> BlogId { get; set; }
    }
}
