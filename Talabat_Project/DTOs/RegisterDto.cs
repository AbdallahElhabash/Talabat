using System.ComponentModel.DataAnnotations;

namespace Talabat_Project.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage="Password must contains 1 UpperCase , 1 LowerCase , 1 Digit , 1 Special Character")]
        public string Password { get; set; }
    }
}
