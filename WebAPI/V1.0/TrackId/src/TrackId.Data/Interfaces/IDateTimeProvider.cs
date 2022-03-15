using System;

namespace TrackId.Data.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; set; }
}