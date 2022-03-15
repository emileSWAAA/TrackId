using System.ComponentModel.DataAnnotations;
using System;

namespace TrackId.Contracts.Artist.Put
{
    public class PutArtistRequest : IRequestContract
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string CountryCode { get; set; }
    }
}
