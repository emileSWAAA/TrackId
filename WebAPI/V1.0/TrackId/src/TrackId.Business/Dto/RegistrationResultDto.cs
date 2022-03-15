using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TrackId.Business.Interfaces;

namespace TrackId.Business.Dto
{
    public class RegistrationResultDto : ISucceedable, IHasErrorList
    {
        public RegistrationResultDto(UserDto user)
        {
            SetFields(user, true, null);
        }

        public RegistrationResultDto(UserDto user, bool succeeded = true, string error = null)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                SetFields(user, succeeded, new IdentityError[]
                {
                    new IdentityError() { Code = "Custom", Description = error }
                });
            }
            else
            {
                SetFields(user, succeeded);
            }

        }

        public RegistrationResultDto(UserDto user, bool succeeded = true, IEnumerable<IdentityError> errors = null)
        {
            SetFields(user, succeeded, errors);
        }

        private void SetFields(UserDto user, bool succeeded = true, IEnumerable<IdentityError> errors = null)
        {
            User = user;
            Succeeded = succeeded;

            if (errors != null && errors.Any())
            {
                Errors = errors.Select(err => err.Description);
            }

        }

        public UserDto User { get; set; }

        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
