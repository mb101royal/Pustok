using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.AuthVM
{
    public class LoginVM
    {
        [Required]
        public string? UsernameOrEmail { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
