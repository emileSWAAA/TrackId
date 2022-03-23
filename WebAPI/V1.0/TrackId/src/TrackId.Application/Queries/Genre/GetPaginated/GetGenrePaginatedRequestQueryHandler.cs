using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackId.Business.Services;
using TrackId.Common.Constants;
using TrackId.Contracts.Genre;
using TrackId.Contracts.Genre.GetPaginated;
using TrackId.Data.Wrappers;

namespace TrackId.Application.Queries.Genre.GetPaginated
{
    public class GetGenrePaginatedRequestQuery : IRequest<GetGenrePaginatedQueryResult>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }

    public class GetGenrePaginatedRequestQueryHandler : IRequestHandler<GetGenrePaginatedRequestQuery, GetGenrePaginatedQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetGenrePaginatedRequestQueryHandler> _logger;
        private readonly IGenreService _genreService;

        public GetGenrePaginatedRequestQueryHandler(
            IGenreService genreService,
            ILogger<GetGenrePaginatedRequestQueryHandler> logger,
            IMapper mapper)
        {
            _genreService = genreService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetGenrePaginatedQueryResult> Handle(GetGenrePaginatedRequestQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PageIndex < 0 || request.PageSize < 1 || request.PageSize > ApplicationConstants.MaxPageSize)
                {
                    return new GetGenrePaginatedQueryResult(RequestErrorType.ValidationError, ErrorConstants.Genre.InvalidParameters);
                }

                var pagedList = await _genreService.GetPaginatedListAsync(request.PageIndex, request.PageSize, cancellationToken);
                if (pagedList is null || pagedList.TotalCount == 0)
                {
                    return new GetGenrePaginatedQueryResult(RequestErrorType.NotFound, ErrorConstants.Genre.NoGenresFound);
                }

                var response = new GetGenrePaginatedResponse { Result = _mapper.Map<PaginatedList<GenreViewModel>>(pagedList) };
                return new GetGenrePaginatedQueryResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong handling '{0}'", nameof(GetGenrePaginatedRequestQueryHandler));
                return new GetGenrePaginatedQueryResult(RequestErrorType.Unknown, ErrorConstants.GeneralError);
            }
        }
    }

    public class GetGenrePaginatedQueryResult : BaseQueryResponse<GetGenrePaginatedResponse>
    {
        public GetGenrePaginatedQueryResult(GetGenrePaginatedResponse result)
            : base(result) { }

        public GetGenrePaginatedQueryResult(RequestErrorType errorType, string errorMessage)
            : base(errorType, errorMessage) { }

        public GetGenrePaginatedQueryResult(bool success, RequestErrorType errorType, string errorMessage)
            : base(success, errorType, errorMessage) { }
    }
}
