using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrackId.Contracts.Models.User
{
    public class LoginRequest : IRequestContract
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
