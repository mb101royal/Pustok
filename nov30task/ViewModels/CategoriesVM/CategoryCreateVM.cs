using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.CategoriesVM
{
    public class CategoryCreateVM
    {
        [Required, MaxLength(16)]
        public string Name { get; set; }
    }
}
