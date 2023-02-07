using System.Threading;
using MediatR;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto.Genre;
using TrackId.Business.Services;
using TrackId.Common.Constants;
using TrackId.Contracts.Genre.GetById;

namespace TrackId.Application.Queries.Genre.GetById
{
    public class GetByIdGenreQuery : IRequest<GetGenrePaginatedQueryResult>
    {
        public Guid Id { get; set; }
    }

    public class GetGenreByIdQueryHandler : IRequestHandler<GetByIdGenreQuery, GetGenrePaginatedQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetGenreByIdQueryHandler> _logger;
        private readonly IGenreService _genreService;

        public GetGenreByIdQueryHandler(
            IMapper mapper,
            ILogger<GetGenreByIdQueryHandler> logger,
            IGenreService genreService)
        {
            _mapper = mapper;
            _logger = logger;
            _genreService = genreService;
        }

        public async Task<GetGenrePaginatedQueryResult> Handle(GetByIdGenreQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            if (id.Equals(Guid.Empty))
            {
                return new GetGenrePaginatedQueryResult(RequestErrorType.NotFound, ErrorConstants.Genre.NotFound);
            }

            if (await _genreService.GetByIdAsync(id, cancellationToken) is not GenreDto genreDto)
            {
                return new GetGenrePaginatedQueryResult(RequestErrorType.NotFound, ErrorConstants.Genre.NotFound);
            }

            if (_mapper.Map<GetGenreByIdResponse>(genreDto) is not GetGenreByIdResponse response)
            {
                _logger.LogError($"Could not map genre with Id: '{genreDto.Id}' from {nameof(GenreDto)} to {nameof(GetGenreByIdResponse)}.");
                return new GetGenrePaginatedQueryResult(RequestErrorType.NotFound, ErrorConstants.Genre.NotFound);
            }

            return new GetGenrePaginatedQueryResult(response);
        }
    }

    public class GetGenrePaginatedQueryResult : BaseQueryResponse<GetGenreByIdResponse>
    {
        public GetGenrePaginatedQueryResult(GetGenreByIdResponse result)
            : base(result) { }

        public GetGenrePaginatedQueryResult(RequestErrorType errorType, string errorMessage)
            : base(errorType, errorMessage) { }

        public GetGenrePaginatedQueryResult(bool success, RequestErrorType errorType, string errorMessage)
            : base(success, errorType, errorMessage) { }
    }
}
