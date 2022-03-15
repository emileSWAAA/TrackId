using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TrackId.WebAPI.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly IMapper Mapper;

        public BaseApiController(IMapper mapper)
        {
            Mapper = mapper;
        }

    }
}
