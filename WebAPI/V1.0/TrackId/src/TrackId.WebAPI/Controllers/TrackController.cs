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

        public TrackController(
            IMapper mapper,
            ILogger<TrackController> logger,
            IMediator mediator)
            : base(mapper, mediator)
        {
            _logger = logger;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetByIdTrackResponse>> GetById(Guid id)
        {
            try
            {
                var result = await Mediator.Send(new GetByIdTrackRequestQuery()
                {
                    Id = id
                }, HttpContext.RequestAborted);

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
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await Mediator.Send(new GetTrackRequestQuery()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                }, HttpContext.RequestAborted);

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
        public async Task<ActionResult<PostTrackResponse>> Post([FromBody] PostTrackRequest request)
        {
            try
            {
                var result = await Mediator.Send(new PostTrackCommand
                {
                    Title = request.Title,
                    Type = request.Type,
                    Artists = request.Artists
                }, HttpContext.RequestAborted);

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
        public async Task<ActionResult<PutTrackResponse>> Put([FromBody] PutTrackRequest request)
        {
            try
            {
                var result = await Mediator.Send(new PutTrackCommand
                {
                    Artists = request.Artists,
                    Id = request.Id,
                    Title = request.Title,
                    Type = request.TrackType
                }, HttpContext.RequestAborted);

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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    return NotFound();
                }

                var result = await Mediator.Send(new DeleteTrackCommand()
                {
                    Id = id
                }, HttpContext.RequestAborted);

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
        public async Task<IActionResult> AddArtists([FromRoute] Guid id, [FromBody] AddArtistsRequest request)
        {
            try
            {
                var result = await Mediator.Send(new AddArtistsCommand()
                {
                    Artists = request.Artists,
                    TrackId = id,
                }, HttpContext.RequestAborted);

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
