using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Contracts.Track.Delete;

namespace TrackId.Application.Commands.Track.Delete
{
    public class DeleteTrackCommand : IRequest<DeleteTrackCommandResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand, DeleteTrackCommandResult>
    {
        private readonly ITrackService _trackService;

        public DeleteTrackCommandHandler(ITrackService trackService)
        {
            _trackService = trackService;
        }

        public async Task<DeleteTrackCommandResult> Handle(DeleteTrackCommand request, CancellationToken cancellationToken)
        {
            if (request.Id.Equals(Guid.Empty))
            {
                return new DeleteTrackCommandResult(RequestErrorType.ValidationError, "Invalid parameters.");
            }

            if (!await _trackService.DeleteAsync(request.Id, cancellationToken))
            {
                return new DeleteTrackCommandResult(RequestErrorType.NotCreated, "Unable to delete track.");
            }

            return new DeleteTrackCommandResult(new DeleteTrackResponse());
        }
    }

    public class DeleteTrackCommandResult : BaseQueryResponse<DeleteTrackResponse>
    {
        public DeleteTrackCommandResult(DeleteTrackResponse result) : base(result)
        {
        }

        public DeleteTrackCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public DeleteTrackCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
