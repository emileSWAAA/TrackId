using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TrackId.Application.Commands.Auth.Register
{
    public class RegisterCommand : IRequest<RegisterCommandResult>
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
