using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrackId.Application.Commands.Track.Delete;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Contracts.Artist.Delete;

namespace TrackId.Application.Commands.Artist.Delete
{
    public class DeleteArtistCommand : IRequest<DeleteArtistCommandResult>
    {
        public Guid Id { get; set; }
    }


    public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, DeleteArtistCommandResult>
    {
        private readonly IArtistService _artistService;

        public DeleteArtistCommandHandler(IArtistService artistService)
        {
            _artistService = artistService;
        }

        public async Task<DeleteArtistCommandResult> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
        {
            if (request.Id.Equals(Guid.Empty))
            {
                return new DeleteArtistCommandResult(RequestErrorType.ValidationError, "Invalid parameters.");
            }

            if (!await _artistService.DeleteAsync(request.Id, cancellationToken))
            {
                return new DeleteArtistCommandResult(RequestErrorType.NotCreated, "Unable to delete artist.");
            }

            return new DeleteArtistCommandResult(new DeleteArtistResponse());
        }
    }

    public class DeleteArtistCommandResult : BaseQueryResponse<DeleteArtistResponse>
    {
        public DeleteArtistCommandResult(DeleteArtistResponse result) : base(result)
        {
        }

        public DeleteArtistCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public DeleteArtistCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
