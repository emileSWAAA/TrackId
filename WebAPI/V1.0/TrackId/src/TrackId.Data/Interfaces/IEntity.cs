using System;

namespace TrackId.Data.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}