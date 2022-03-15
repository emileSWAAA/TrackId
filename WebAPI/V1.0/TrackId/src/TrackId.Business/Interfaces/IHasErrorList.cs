using System.Collections.Generic;

namespace TrackId.Business.Interfaces
{
    public interface IHasErrorList
    {
        IEnumerable<string> Errors { get; set; }
    }
}