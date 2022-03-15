using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackId.Application.Commands.Track.AddArtists;
using TrackId.Application.Commands.Track.Delete;
using TrackId.Application.Commands.Track.Post;
using TrackId.Application.Commands.Track.Put;
using TrackId.Application.Queries;
using TrackId.Application.Queries.Track;
using TrackId.Contracts.Track;
using TrackId.Contracts.Track.AddArtist;
using TrackId.Data.Wrappers;

namespace TrackId.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : BaseApiController
    {
        private readonly ILogger<TrackController> _logger;
        private readonly IMediator _mediator;

        public TrackController(
            IMapper mapper,
            ILogger<TrackController> logger,
            IMediator mediator)
            : base(mapper)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetByIdTrackResponse>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdTrackRequestQuery()
                {
                    Id = id
                }, cancellationToken);

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<GetByIdTrackResponse>>> GetPaginated(
            CancellationToken cancellationToken,
            int pageIndex = 0,
            int pageSize = 20)
        {
            try
            {
                var result = await _mediator.Send(new GetTrackRequestQuery()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                }, cancellationToken);

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(result.Result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PostTrackResponse>> Post([FromBody] PostTrackRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new PostTrackCommand
                {
                    Title = request.Title,
                    Type = request.Type,
                    Artists = request.Artists
                }, cancellationToken);

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult<PutTrackResponse>> Put([FromBody] PutTrackRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new PutTrackCommand
                {
                    Artists = request.Artists,
                    Id = request.Id,
                    Title = request.Title,
                    Type = request.TrackType
                },
                    cancellationToken);

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    return NotFound();
                }

                var result = await _mediator.Send(new DeleteTrackCommand()
                {
                    Id = id
                }, cancellationToken);

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:guid}/artist")]
        public async Task<IActionResult> AddArtists([FromRoute] Guid id, [FromBody] AddArtistsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new AddArtistsCommand()
                {
                    Artists = request.Artists,
                    TrackId = id,
                }, cancellationToken);

                if (result is null)
                {
                    return NotFound();
                }

                if (!result.Success && result.Errors.Any(err => err.Type == RequestErrorType.NotFound))
                {
                    return NotFound();
                }

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
