using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackId.Application.Commands.Genre.Delete;
using TrackId.Application.Commands.Genre.Post;
using TrackId.Application.Queries.Genre.GetById;
using TrackId.Application.Queries.Genre.GetPaginated;
using TrackId.Contracts.Genre.GetById;
using TrackId.Contracts.Genre.GetPaginated;
using TrackId.Contracts.Genre.Post;

namespace TrackId.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : BaseApiController
    {
        private readonly ILogger<GenreController> _logger;

        public GenreController(IMapper mapper, ILogger<GenreController> logger, IMediator mediator)
            : base(mapper, mediator)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<GetGenrePaginatedResponse>> GetAllPaginated(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await Mediator.Send(new GetGenrePaginatedRequestQuery
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetGenreByIdResponse>> GetById(Guid id)
        {
            try
            {
                var result = await Mediator.Send(new GetByIdGenreQuery
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

        [HttpGet("{id}/tracks")]
        public async Task<ActionResult<GetGenreByIdResponse>> GetByIdWithTracks(
            [FromRoute] Guid id,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await Mediator.Send(new GetGenreByIdWithTracksQuery
                {
                    Id = id,
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PostGenreResponse>> Post([FromBody] PostGenreRequest request)
        {
            try
            {
                var result = await Mediator.Send(new PostGenreCommand
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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    return NotFound();
                }

                var result = await Mediator.Send(new DeleteGenreCommand
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
    }
}
