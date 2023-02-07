using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Contracts.Artist;

namespace TrackId.Application.Queries.Artist.GetById
{
    public class GetByIdArtistQuery : IRequest<GetByIdArtistQueryResult>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdArtistQueryHandler : IRequestHandler<GetByIdArtistQuery, GetByIdArtistQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdArtistQueryHandler> _logger;
        private readonly IArtistService _artistService;

        public GetByIdArtistQueryHandler(
            IArtistService artistService,
            ILogger<GetByIdArtistQueryHandler> logger,
            IMapper mapper)
        {
            _artistService = artistService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetByIdArtistQueryResult> Handle(GetByIdArtistQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            if (id.Equals(Guid.Empty))
            {
                return new GetByIdArtistQueryResult(RequestErrorType.ValidationError, "Forbidden guid.");
            }

            if (await _artistService.GetByIdAsync(id, cancellationToken) is not ArtistDto artist)
            {
                return new GetByIdArtistQueryResult(RequestErrorType.NotFound, "Could not find track.");
            }

            if (_mapper.Map<GetByIdArtistResponse>(artist) is not GetByIdArtistResponse response)
            {
                _logger.LogError($"Could not map track with Id: '{artist.Id}' from {nameof(ArtistDto)} to {nameof(GetByIdArtistResponse)}.");
                return new GetByIdArtistQueryResult(RequestErrorType.NotFound, "Could not find track.");
            }

            return new GetByIdArtistQueryResult(response);
        }
    }

    public class GetByIdArtistQueryResult : BaseQueryResponse<GetByIdArtistResponse>
    {
        public GetByIdArtistQueryResult(GetByIdArtistResponse result) : base(result)
        {
        }

        public GetByIdArtistQueryResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public GetByIdArtistQueryResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
