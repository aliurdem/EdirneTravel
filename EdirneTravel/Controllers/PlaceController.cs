using EdirneTravel.Application.Services;
using EdirneTravel.Application.Services.Base;
using EdirneTravel.Controllers.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdirneTravel.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaceController : BaseController<Place, PlaceDto>
    {
        public PlaceController(IService<Place, PlaceDto> service) : base(service)
        {
        }
    }
}
