using nov30task.Models;
using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.CategoriesVM
{
    public class CategoryCreateVM
    {
        [Required, MaxLength(16)]
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
    }
}
