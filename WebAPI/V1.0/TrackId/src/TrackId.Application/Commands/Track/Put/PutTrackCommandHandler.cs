using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Common.Enum;
using TrackId.Contracts.Track;

namespace TrackId.Application.Commands.Track.Put
{
    public class PutTrackCommand : IRequest<PutTrackCommandResult>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public TrackType Type { get; set; }

        public IEnumerable<Guid> Artists { get; set; }

        public Guid? GenreId { get; set; }
    }

    public class PutTrackCommandHandler : IRequestHandler<PutTrackCommand, PutTrackCommandResult>
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public PutTrackCommandHandler(
            ITrackService trackService,
            IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        public async Task<PutTrackCommandResult> Handle(PutTrackCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TrackDto>(request);
            if (dto is null)
            {
                return new PutTrackCommandResult(RequestErrorType.ValidationError, "Invalid parameters.");
            }

            var result = await _trackService.UpdateAsync(dto, cancellationToken);
            if (result is null)
            {
                return new PutTrackCommandResult(RequestErrorType.NotCreated, "Unable to update track.");
            }

            return new PutTrackCommandResult(_mapper.Map<PutTrackResponse>(result));
        }
    }

    public class PutTrackCommandResult : BaseQueryResponse<PutTrackResponse>
    {
        public PutTrackCommandResult(PutTrackResponse result) : base(result)
        {
        }

        public PutTrackCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public PutTrackCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
