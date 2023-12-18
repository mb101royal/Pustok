using System.ComponentModel.DataAnnotations;

namespace nov30task.Models
{
    public class Tag
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }

        public ICollection<BlogTag>? BlogsTag { get; set; }
    }
}
