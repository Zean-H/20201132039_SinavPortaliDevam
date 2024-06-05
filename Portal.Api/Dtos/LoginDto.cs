using System.ComponentModel.DataAnnotations;

namespace _20201132039_SinavPortali.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
