using MediatR;

namespace TrackId.Application.Commands.Auth.Login
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
