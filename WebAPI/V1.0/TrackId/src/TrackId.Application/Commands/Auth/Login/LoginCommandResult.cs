using TrackId.Application.Queries;
using TrackId.Contracts.Models.User;

namespace TrackId.Application.Commands.Auth.Login
{
    public class LoginCommandResult : BaseQueryResponse<LoginResponse>
    {
        public LoginCommandResult(LoginResponse result)
            : base(result)
        {
        }

        public LoginCommandResult(RequestErrorType errorType, string errorMessage)
            : base(errorType, errorMessage)
        {
        }

        public LoginCommandResult(bool success, RequestErrorType errorType, string errorMessage)
            : base(success, errorType, errorMessage)
        {
        }
    }
}
