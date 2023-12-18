using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.TagsVM
{
    public class TagCreateVM
    {
        [Required]
        public string? Name { get; set; }
    }
}
