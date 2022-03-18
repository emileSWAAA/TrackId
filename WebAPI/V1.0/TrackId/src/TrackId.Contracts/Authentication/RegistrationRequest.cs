using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrackId.Contracts.Models.User
{
    public class RegistrationRequest : IRequestContract
    {
        [PasswordPropertyText]
        public string Password { get; set; }

        [PasswordPropertyText]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
