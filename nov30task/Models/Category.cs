using System.ComponentModel.DataAnnotations;

namespace nov30task.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(16)]
<<<<<<< HEAD
        public string? Name { get; set; }
        public IEnumerable<Book>? Books { get; set; }
=======
        public string Name { get; set; }
        public ICollection<Book>? Books { get; set; } = new List<Book>();
        public int? ParentCategoryId { get; set; }
>>>>>>> 1c03025e5f05c7c77e8ab41bf5db0c598bebb33f
        public bool IsDeleted { get; set; }
        public Category? ParentCategory { get; set; }
    }
}
