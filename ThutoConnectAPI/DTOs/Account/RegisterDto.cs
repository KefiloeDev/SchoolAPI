using System.ComponentModel.DataAnnotations;

namespace ThutoConnectAPI.DTOs.Account
{
    public class RegisterDto
    {
        [Required]

        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage ="Password must be at least {2}, and maximum {1} charecters")]
        public string Password { get; set; }
    }
}
