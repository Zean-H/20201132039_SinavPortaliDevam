using System.ComponentModel.DataAnnotations;

namespace _20201132039_SinavPortali.Dtos
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public required string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
