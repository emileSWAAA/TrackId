using TrackId.Application.Queries;
using TrackId.Contracts.Models.User;

namespace TrackId.Application.Commands.Auth.Register
{
    public class RegisterCommandResult : BaseQueryResponse<RegistrationResponse>
    {
        public RegisterCommandResult(RegistrationResponse result) : base(result)
        {
        }

        public RegisterCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public RegisterCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
