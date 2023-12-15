using System.ComponentModel.DataAnnotations;

namespace nov30task.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(16)]
        public string Name { get; set; }
        public ICollection<Book>? Books { get; set; } = new List<Book>();
        public int? ParentCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public Category? ParentCategory { get; set; }
    }
}
