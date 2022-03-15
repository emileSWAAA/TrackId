using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Contracts.Artist.Post;

namespace TrackId.Application.Commands.Artist.Post
{
    public class PostArtistCommandHandler : IRequestHandler<PostArtistCommand, PostArtistCommandResult>
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public PostArtistCommandHandler(
            IArtistService artistService,
            IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        public async Task<PostArtistCommandResult> Handle(PostArtistCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ArtistDto>(request.Request);
            if (dto is null)
            {
                return new PostArtistCommandResult(RequestErrorType.ValidationError, "Invalid parameters.");
            }

            var result = await _artistService.AddAsync(dto, cancellationToken);
            if (result is null)
            {
                return new PostArtistCommandResult(RequestErrorType.NotCreated, "Unable to create artist.");
            }

            return new PostArtistCommandResult(_mapper.Map<PostArtistResponse>(result));
        }
    }
}
