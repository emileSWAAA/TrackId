using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrackId.Business.Dto
{
    public class RegistrationDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }

        [PasswordPropertyText]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
