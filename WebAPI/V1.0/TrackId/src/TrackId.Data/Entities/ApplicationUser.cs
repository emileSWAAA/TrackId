using System;
using Microsoft.AspNetCore.Identity;
using TrackId.Common.Interfaces;
using TrackId.Data.Interfaces;

namespace TrackId.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity, ICreateDateTime
    {
        public DateTime CreateDateTime { get; set; }

    }
}
