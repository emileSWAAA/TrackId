using System.Collections.Generic;
using System.Linq;
using TrackId.Business.Dto;
using TrackId.Common.Constants;

namespace TrackId.Business.Extensions
{
    public static class ArtistExtensions
    {
        public static string GetName(this ArtistDto artist)
        {
            if (artist == null || string.IsNullOrWhiteSpace(artist.Name))
            {
                return ArtistConstants.Name.Unknown;
            }

            return artist.Name;
        }

        public static string GetArtistsPretty(this IEnumerable<ArtistDto> artists)
        {
            if (artists == null || !artists.Any())
            {
                return ArtistConstants.Name.Unknown;
            }

            if (artists.Count() == 1)
            {
                return artists.First().GetName();
            }

            return string.Join(" & ", artists);
        }

        private static string ConsolidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return ArtistConstants.Name.Unknown;
            }

            return name;
        }

        private static IEnumerable<ArtistDto> ConsolidateNames(this IEnumerable<ArtistDto> artists)
        {
            foreach (var artist in artists)
            {
                if (string.IsNullOrWhiteSpace(artist.Name))
                {
                    artist.Name = ArtistConstants.Name.Unknown;
                }
            }

            return artists;
        }
    }
}
