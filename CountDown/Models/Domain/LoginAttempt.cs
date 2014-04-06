using System.ComponentModel.DataAnnotations;

namespace CountDown.Models.Domain
{
    public class LoginAttempt
    {
        [Required(ErrorMessage = "Email address is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}