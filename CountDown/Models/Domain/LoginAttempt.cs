using System.ComponentModel.DataAnnotations;

namespace CountDown.Models.Domain
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    public class LoginAttempt
    {
        [Required(ErrorMessage = "Email address is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}