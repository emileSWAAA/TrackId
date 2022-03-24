using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TrackId.Business.Services;
using TrackId.Contracts.Track;
using TrackId.Data.Wrappers;

namespace TrackId.Application.Queries.Track
{
    public class GetTrackRequestQuery : IRequest<GetTrackQueryResult>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }

    public class GetTrackRequestQueryHandler : IRequestHandler<GetTrackRequestQuery, GetTrackQueryResult>
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public GetTrackRequestQueryHandler(
            ITrackService trackService,
            IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        public async Task<GetTrackQueryResult> Handle(GetTrackRequestQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex < 0 || request.PageSize < 1)
            {
                return new GetTrackQueryResult(RequestErrorType.ValidationError, "Invalid pagination parameters.");
            }

            var pagedList = await _trackService.GetPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            if (pagedList is null || !pagedList.Items.Any())
            {
                return new GetTrackQueryResult(RequestErrorType.NotFound, "No tracks found.");
            }

            var response = new GetTrackPaginatedResponse
            {
                Result = _mapper.Map<PaginatedList<TrackResult>>(pagedList)
            };

            return new GetTrackQueryResult(response);
        }
    }

    public class GetTrackQueryResult : BaseQueryResponse<GetTrackPaginatedResponse>
    {
        public GetTrackQueryResult(GetTrackPaginatedResponse result) : base(result) { }

        public GetTrackQueryResult(bool success, RequestErrorType errorType, string errorMessage)
            : base(success, errorType, errorMessage) { }

        public GetTrackQueryResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage) { }
    }
}
