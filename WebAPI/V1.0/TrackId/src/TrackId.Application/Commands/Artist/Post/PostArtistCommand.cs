using MediatR;
using TrackId.Contracts.Artist.Post;

namespace TrackId.Application.Commands.Artist.Post
{
    public class PostArtistCommand : IRequest<PostArtistCommandResult>
    {
        public PostArtistRequest Request { get; set; }
    }
}
