using System.ComponentModel.DataAnnotations;

namespace CountDown.Models.Domain
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class LoginAttempt
    {
        [Required(ErrorMessage = "Email address is required.")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public virtual string Password { get; set; }
    }
}