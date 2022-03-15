using System.ComponentModel.DataAnnotations;
using System;
using MediatR;

namespace TrackId.Application.Commands.Artist.Put
{
    public class PutArtistCommand : IRequest<PutArtistCommandResult>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string CountryCode { get; set; }
    }
}
