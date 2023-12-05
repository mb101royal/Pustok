using System.ComponentModel.DataAnnotations;

namespace nov30task.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(16)]
        public string Name { get; set; }
        public IEnumerable<Product>? Products { get; set; }

        public bool IsDeleted { get; set; }
        public int ParentCategoryId { get; set; }
    }
}
