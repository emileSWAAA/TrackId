using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Contracts.Artist.Put;

namespace TrackId.Application.Commands.Artist.Put
{
    public class PutArtistCommandHandler : IRequestHandler<PutArtistCommand, PutArtistCommandResult>
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public PutArtistCommandHandler(
            IArtistService artistService,
            IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        public async Task<PutArtistCommandResult> Handle(PutArtistCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ArtistDto>(request);
            if (dto is null)
            {
                return new PutArtistCommandResult(RequestErrorType.ValidationError, "Invalid parameters.");
            }

            var result = await _artistService.UpdateAsync(dto, cancellationToken);
            if (result is null)
            {
                return new PutArtistCommandResult(RequestErrorType.NotCreated, "Unable to update track.");
            }

            return new PutArtistCommandResult(_mapper.Map<PutArtistResponse>(result));
        }
    }
}
