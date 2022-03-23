using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackId.Application.Queries.Genre.GetPaginated;
using TrackId.Contracts.Genre.GetPaginated;

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
                var result = await Mediator.Send(new GetGenrePaginatedRequestQuery()
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
    }
}
