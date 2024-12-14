using EdirneTravel.Application.Services;
using EdirneTravel.Application.Services.Base;
using EdirneTravel.Controllers.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Dtos.TravelRoute;
using EdirneTravel.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EdirneTravel.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TravelRouteController : BaseController<TravelRoute,TravelRouteDto>
    {
        private readonly ITravelRouteService _travelRouteService;
        public TravelRouteController(ITravelRouteService service) : base(service)
        {
            _travelRouteService = service;

        }

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] TravelRouteDto travelRouteDto, CancellationToken cancellationToken)
        {
            var result = _travelRouteService.SaveTravelRouteWithPlaces(travelRouteDto);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpPatch]
        public override async Task<IActionResult> Update([FromBody] TravelRouteDto travelRouteDto, CancellationToken cancellationToken)
        {
            var result = _travelRouteService.SaveTravelRouteWithPlaces(travelRouteDto);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(int id)
        {
            var result = _travelRouteService.GetTravelRouteWithPlacesById(id);

            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
    }
}
