using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackId.Application.Commands.Artist.Delete;
using TrackId.Application.Commands.Artist.Post;
using TrackId.Application.Commands.Artist.Put;
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

        public ArtistController(
            IMapper mapper,
            ILogger<ArtistController> logger,
            IMediator mediator) : base(mapper, mediator)
        {
            _logger = logger;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetByIdArtistResponse>> GetById(Guid id)
        {
            try
            {
                var result = await Mediator.Send(new GetByIdArtistQuery
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<GetArtistPaginatedResponse>> GetAllPaginated(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await Mediator.Send(new GetArtistRequestQuery
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
        public async Task<ActionResult<PostArtistResponse>> Post(
            [FromBody] PostArtistRequest request)
        {
            try
            {
                var result = await Mediator.Send(new PostArtistCommand
                {
                    Request = request
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

                var result = await Mediator.Send(new DeleteArtistCommand
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

        [HttpPut]
        public async Task<ActionResult<PutTrackResponse>> Put([FromBody] PutArtistRequest request)
        {
            try
            {
                var result = await Mediator.Send(new PutArtistCommand
                {
                    Id = request.Id,
                    CountryCode = request.CountryCode,
                    Name = request.Name
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
    }
}
