using System.Collections.Generic;
using TrackId.Business.Interfaces;

namespace TrackId.Business.Dto
{
    public class AuthenticationResultDto : ISucceedable, IHasErrorList
    {
        public AuthenticationResultDto(
            UserDto user,
            bool succeeded = true,
            IEnumerable<string> errors = null
            )
        {
            Succeeded = user != null && succeeded;
            Errors = errors;
        }

        public UserDto User { get; set; }

        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
