using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackId.Application.Queries;
using TrackId.Business.Services;
using TrackId.Common.Constants;
using TrackId.Contracts.Genre.Delete;

namespace TrackId.Application.Commands.Genre.Delete
{
    public class DeleteGenreCommand : IRequest<DeleteGenreCommandResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, DeleteGenreCommandResult>
    {
        private readonly ILogger<DeleteGenreCommandHandler> _logger;
        private readonly IGenreService _genreService;

        public DeleteGenreCommandHandler(
            ILogger<DeleteGenreCommandHandler> logger,
            IGenreService genreService)
        {
            _logger = logger;
            _genreService = genreService;
        }

        public async Task<DeleteGenreCommandResult> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.Equals(Guid.Empty))
                {
                    return new DeleteGenreCommandResult(RequestErrorType.ValidationError, ErrorConstants.Genre.InvalidParameters);
                }

                if (!await _genreService.DeleteAsync(request.Id, cancellationToken))
                {
                    return new DeleteGenreCommandResult(RequestErrorType.NotCreated, ErrorConstants.Genre.NotDeleted);
                }

                return new DeleteGenreCommandResult(new DeleteGenreResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong handling command '{0}'", nameof(DeleteGenreCommandResult));
                return new DeleteGenreCommandResult(RequestErrorType.Unknown, ErrorConstants.GeneralError);
            }
        }
    }

    public class DeleteGenreCommandResult : BaseQueryResponse<DeleteGenreResponse>
    {
        public DeleteGenreCommandResult(DeleteGenreResponse result)
            : base(result) { }

        public DeleteGenreCommandResult(RequestErrorType errorType, string errorMessage)
            : base(errorType, errorMessage) { }

        public DeleteGenreCommandResult(bool success, RequestErrorType errorType, string errorMessage)
            : base(success, errorType, errorMessage) { }
    }
}
