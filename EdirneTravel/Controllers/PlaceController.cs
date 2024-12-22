using EdirneTravel.Application.Services;
using EdirneTravel.Application.Services.Base;
using EdirneTravel.Controllers.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Entities.Base;
using EdirneTravel.Models.Utilities.Results;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Create([FromBody] PlaceDto model, CancellationToken cancellationToken)
        {
            var result = _manager.Insert(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Update([FromBody] PlaceDto model, CancellationToken cancellationToken)
        {
            var result = _manager.Update(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override IAppResult Delete(int id)
        {
            _manager.Delete(id);
            return new SuccessResult("ENTITY_DELETED");
        }
    }
}
