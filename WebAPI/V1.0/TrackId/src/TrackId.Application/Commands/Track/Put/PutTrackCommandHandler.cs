using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Contracts.Track;

namespace TrackId.Application.Commands.Track.Put
{
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
}
