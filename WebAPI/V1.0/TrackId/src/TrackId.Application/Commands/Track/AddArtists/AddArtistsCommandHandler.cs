using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Contracts.Track.AddArtist;

namespace TrackId.Application.Commands.Track.AddArtists
{
    public class AddArtistsCommandHandler : IRequestHandler<AddArtistsCommand, AddArtistsCommandResult>
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;
        private readonly ILogger<AddArtistsCommandHandler> _logger;

        public AddArtistsCommandHandler(
            ITrackService trackService,
            IMapper mapper,
            ILogger<AddArtistsCommandHandler> logger)
        {
            _trackService = trackService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddArtistsCommandResult> Handle(AddArtistsCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Artists == null || request.TrackId.Equals(Guid.Empty))
            {
                return new AddArtistsCommandResult(RequestErrorType.InvalidArgument, "Invalid arguments");
            }

            if (await _trackService.AddArtistsAsync(request.TrackId, request.Artists, cancellationToken) is not TrackDto trackDto)
            {
                return new AddArtistsCommandResult(RequestErrorType.NotFound, $"Unable to find track with id {request.TrackId}");
            }

            var result = _mapper.Map<AddArtistsResponse>(trackDto);
            return new AddArtistsCommandResult(result);
        }
    }
}
