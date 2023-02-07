using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TrackId.WebAPI.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly IMediator Mediator;

        public BaseApiController(IMapper mapper, IMediator mediator)
        {
            Mapper = mapper;
            Mediator = mediator;
        }

    }
}
