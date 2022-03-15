using System;
using TrackId.Data.Interfaces;

namespace TrackId.Data.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow { get; set; } = DateTime.UtcNow;
    }
}
