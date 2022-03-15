using System;

namespace TrackId.Contracts.Models.User
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }
    }
}
