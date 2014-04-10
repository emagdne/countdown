using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountDown.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column("first_name", TypeName = "VARCHAR")]
        [Required(ErrorMessage="You must provide a first name.")]
        [StringLength(50, ErrorMessage = "The first name must be from 1 to 50 characters in length.")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "VARCHAR")]
        [StringLength(50, ErrorMessage = "The last name must be from 0 to 50 characters in length.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must provide an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must confirm your email address.")]
        [Compare("Email", ErrorMessage = "Emails do not match.")]
        [NotMapped]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "You must provide a password.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The password must be from 4 to 50 characters in length.")]
        [NotMapped]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [StringLength(68, MinimumLength = 68, ErrorMessage = "The hash must be 68 characters long.")]
        public string Hash { get; set; }
    }
}