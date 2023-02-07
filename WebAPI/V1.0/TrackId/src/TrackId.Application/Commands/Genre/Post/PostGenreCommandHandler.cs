using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackId.Application.Queries;
using TrackId.Business.Dto.Genre;
using TrackId.Business.Services;
using TrackId.Common.Constants;
using TrackId.Contracts.Genre.Post;

namespace TrackId.Application.Commands.Genre.Post
{
    public class PostGenreCommand : IRequest<PostGenreCommandResult>
    {
        public PostGenreRequest Request { get; set; }
    }

    public class PostGenreCommandHandler : IRequestHandler<PostGenreCommand, PostGenreCommandResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PostGenreCommandHandler> _logger;
        private readonly IGenreService _genreService;

        public PostGenreCommandHandler(
            IGenreService genreService,
            ILogger<PostGenreCommandHandler> logger,
            IMapper mapper)
        {
            _genreService = genreService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostGenreCommandResult> Handle(PostGenreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = _mapper.Map<GenreDto>(request.Request);
                if (dto is null)
                {
                    return new PostGenreCommandResult(RequestErrorType.ValidationError, ErrorConstants.Genre.InvalidParameters);
                }

                if (await _genreService.AddAsync(dto, cancellationToken) is not GenreDto genreDto)
                {
                    return new PostGenreCommandResult(RequestErrorType.NotCreated, ErrorConstants.Genre.NotCreated);
                }

                return new PostGenreCommandResult(_mapper.Map<PostGenreResponse>(genreDto));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong handling command '{0}'", nameof(PostGenreCommandResult));
                return new PostGenreCommandResult(RequestErrorType.Unknown, ErrorConstants.GeneralError);
            }
        }
    }

    public class PostGenreCommandResult : BaseQueryResponse<PostGenreResponse>
    {
        public PostGenreCommandResult(PostGenreResponse result) : base(result)
        {
        }

        public PostGenreCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public PostGenreCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
