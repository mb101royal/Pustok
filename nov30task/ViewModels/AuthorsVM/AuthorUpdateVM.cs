using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.AuthorsVM
{
    public class AuthorUpdateVM
    {
        [Required, MaxLength(32)]
        public string? Name { get; set; }
        [MaxLength(42)]
        public string? Surname { get; set; }
    }
}
