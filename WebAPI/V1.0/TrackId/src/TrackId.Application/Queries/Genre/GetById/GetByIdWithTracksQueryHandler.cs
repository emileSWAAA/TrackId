using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto.Genre;
using TrackId.Business.Services;
using TrackId.Common.Constants;
using TrackId.Contracts.Genre.GetById;

namespace TrackId.Application.Queries.Genre.GetById
{
    public class GetGenreByIdWithTracksQuery : IRequest<GetGenreByIdWithTracksResult>
    {
        public Guid Id { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }

    public class GetByIdWithTracksQueryHandler : IRequestHandler<GetGenreByIdWithTracksQuery, GetGenreByIdWithTracksResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdWithTracksQueryHandler> _logger;
        private readonly IGenreService _genreService;

        public GetByIdWithTracksQueryHandler(
            IGenreService genreService,
            ILogger<GetByIdWithTracksQueryHandler> logger,
            IMapper mapper)
        {
            _genreService = genreService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetGenreByIdWithTracksResult> Handle(GetGenreByIdWithTracksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    return new GetGenreByIdWithTracksResult(RequestErrorType.ValidationError, ErrorConstants.Genre.InvalidParameters);
                }

                if (request.PageIndex < 0 || request.PageSize < 1 || request.PageSize > ApplicationConstants.MaxPageSize)
                {
                    return new GetGenreByIdWithTracksResult(RequestErrorType.ValidationError, ErrorConstants.Genre.InvalidParameters);
                }

                if(await _genreService.GetGenreWithTracks(request.Id, request.PageSize, request.PageIndex, cancellationToken) is not GenreDto genreDto)
                {
                    return new GetGenreByIdWithTracksResult(RequestErrorType.NotFound, ErrorConstants.Genre.NotFound);
                }

                return new GetGenreByIdWithTracksResult(_mapper.Map<GetGenreByIdWithTracksResponse>(genreDto));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong handling '{0}'", nameof(GetGenreByIdWithTracksResult));
                return new GetGenreByIdWithTracksResult(RequestErrorType.Unknown, ErrorConstants.GeneralError);
            }
        }
    }

    public class GetGenreByIdWithTracksResult : BaseQueryResponse<GetGenreByIdWithTracksResponse>
    {
        public GetGenreByIdWithTracksResult(GetGenreByIdWithTracksResponse result)
            : base(result) { }

        public GetGenreByIdWithTracksResult(RequestErrorType errorType, string errorMessage)
            : base(errorType, errorMessage) { }

        public GetGenreByIdWithTracksResult(bool success, RequestErrorType errorType, string errorMessage)
            : base(success, errorType, errorMessage) { }
    }
}
