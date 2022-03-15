using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrackId.Contracts.Track;
using AutoMapper;
using TrackId.Business.Services;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto;

namespace TrackId.Application.Queries.Track
{
    public class GetByIdTrackRequestQueryHandler : IRequestHandler<GetByIdTrackRequestQuery, GetByIdTrackQueryResult>
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdTrackRequestQueryHandler> _logger;

        public GetByIdTrackRequestQueryHandler(
            ITrackService trackService,
            IMapper mapper,
            ILogger<GetByIdTrackRequestQueryHandler> logger
            )
        {
            _trackService = trackService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetByIdTrackQueryResult> Handle(GetByIdTrackRequestQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            if (id.Equals(Guid.Empty))
            {
                return new GetByIdTrackQueryResult(success: false, RequestErrorType.ValidationError, "Forbidden guid.");
            }

            var track = await _trackService.GetByIdAsync(id, cancellationToken);
            if (track is null)
            {
                return new GetByIdTrackQueryResult(success: false, RequestErrorType.NotFound, "Could not find track.");
            }

            var response = _mapper.Map<GetByIdTrackResponse>(track);
            if(response is null)
            {
                _logger.LogError($"Could not map track with Id: '{track.Id}' from {nameof(TrackDto)} to {nameof(GetByIdTrackResponse)}.");
                return new GetByIdTrackQueryResult(success: false, RequestErrorType.NotFound, "Could not find track.");
            }

            return new GetByIdTrackQueryResult(response);
        }
    }
}
