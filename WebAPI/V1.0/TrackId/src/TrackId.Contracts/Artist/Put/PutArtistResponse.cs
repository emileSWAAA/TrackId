using System.ComponentModel.DataAnnotations;
using System;

namespace TrackId.Contracts.Artist.Put
{
    public class PutArtistResponse : IResponseContract
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string CountryCode { get; set; }

        public bool IsValidated { get; set; }
    }
}
