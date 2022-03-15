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

            if (await _trackService.GetByIdAsync(request.Id, cancellationToken) is not TrackDto track)
            {
                return new DeleteTrackCommandResult(RequestErrorType.NotFound, "Cannot delete non-existing track.");
            }

            var result = await _trackService.DeleteAsync(request.Id, cancellationToken);
            if (!result)
            {
                return new DeleteTrackCommandResult(RequestErrorType.NotCreated, "Unable to delete track.");
            }

            return new DeleteTrackCommandResult(new DeleteTrackResponse());
        }
    }
}
