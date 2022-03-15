using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackId.Application.Commands.Artist.Delete;
using TrackId.Application.Commands.Artist.Post;
using TrackId.Application.Commands.Artist.Put;
using TrackId.Application.Commands.Track.Put;
using TrackId.Application.Queries.Artist.Get;
using TrackId.Application.Queries.Artist.GetById;
using TrackId.Contracts.Artist;
using TrackId.Contracts.Artist.GetPaginated;
using TrackId.Contracts.Artist.Post;
using TrackId.Contracts.Artist.Put;
using TrackId.Contracts.Track;

namespace TrackId.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : BaseApiController
    {
        private readonly ILogger<ArtistController> _logger;
        private readonly IMediator _mediator;

        public ArtistController(
            IMapper mapper,
            ILogger<ArtistController> logger,
            IMediator mediator) : base(mapper)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetByIdArtistResponse>> GetById(Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdArtistQuery()
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<GetArtistPaginatedResponse>> GetAllPaginated(
            CancellationToken cancellationToken,
            int pageIndex = 0,
            int pageSize = 20)
        {
            try
            {
                var result = await _mediator.Send(new GetArtistRequestQuery()
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
        public async Task<ActionResult<PostArtistResponse>> Post(
            [FromBody] PostArtistRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new PostArtistCommand
                {
                    Request = request
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

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    return NotFound();
                }

                var result = await _mediator.Send(new DeleteArtistCommand()
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

        [HttpPut]
        public async Task<ActionResult<PutTrackResponse>> Put([FromBody] PutArtistRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new PutArtistCommand
                {
                    Id = request.Id,
                    CountryCode = request.CountryCode,
                    Name = request.Name
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
    }
}
