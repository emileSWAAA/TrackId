using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackId.Business.Services;
using TrackId.Common.Constants;
using TrackId.Contracts.Artist.GetPaginated;
using TrackId.Data.Wrappers;

namespace TrackId.Application.Queries.Artist.Get
{
    public class GetArtistRequestQuery : IRequest<GetArtistQueryResult>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }

    public class GetArtistRequestQueryHandler : IRequestHandler<GetArtistRequestQuery, GetArtistQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly IArtistService _artistService;

        public GetArtistRequestQueryHandler(
            IMapper mapper,
            IArtistService artistService)
        {
            _mapper = mapper;
            _artistService = artistService;
        }

        public async Task<GetArtistQueryResult> Handle(GetArtistRequestQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex < 0 || request.PageSize < 1 || request.PageSize > ApplicationConstants.MaxPageSize)
            {
                return new GetArtistQueryResult(RequestErrorType.ValidationError, "Invalid pagination parameters.");
            }

            var pagedList = await _artistService.GetPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            if (pagedList is null || pagedList.TotalCount == 0)
            {
                return new GetArtistQueryResult(RequestErrorType.NotFound, "No artists found.");
            }

            var response = new GetArtistPaginatedResponse
            {
                Result = _mapper.Map<PaginatedList<ArtistResult>>(pagedList)
            };

            return new GetArtistQueryResult(response);
        }
    }

    public class GetArtistQueryResult : BaseQueryResponse<GetArtistPaginatedResponse>
    {
        public GetArtistQueryResult(GetArtistPaginatedResponse result) : base(result)
        {
        }

        public GetArtistQueryResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public GetArtistQueryResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
