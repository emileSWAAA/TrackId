using System;

namespace TrackId.Common.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeleteDateTime { get; set; }
    }
}