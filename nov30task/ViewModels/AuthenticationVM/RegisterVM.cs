using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.AuthenticationVM
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Enter valid name and surname"), MaxLength(64)]
        public string? Fullname { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Enter valid username"), MaxLength(32)]
        public string? Username { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(ConfirmPassword)), RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password should contain: A-Z, a-z, 0-9 and min 6 characters.")]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password), RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password should contain: A-Z, a-z, 0-9 and min 6 characters.")]
        public string? ConfirmPassword { get; set; }
    }
}
