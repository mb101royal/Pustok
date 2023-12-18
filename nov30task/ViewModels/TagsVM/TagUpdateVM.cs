using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.TagsVM
{
    public class TagUpdateVM
    {
        [Required]
        public string? Name { get; set; }
    }
}
