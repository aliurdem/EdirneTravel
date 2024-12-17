using EdirneTravel.Application.Services.Base;
using EdirneTravel.Controllers.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Dtos.UserFav;
using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Entities.Base;
using EdirneTravel.Models.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EdirneTravel.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserFavController : BaseController<UserFav, UserFavDto>
    {
        public UserFavController(IService<UserFav, UserFavDto> service) : base(service)
        {
        }

        [HttpPost]
        [Authorize]
        public override async Task<IActionResult> Create([FromBody] UserFavDto model, CancellationToken cancellationToken)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            model.UserId = userId;

            var result = _manager.Insert(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("/RemoveUserFav")]
        [Authorize]
        public IAppResult RemoveUserFav([FromBody] RemoveUserFavDto model,CancellationToken cancellationToken)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var foundFav = _manager.Select(p => p.UserId == userId && p.PlaceId == model.PlaceId).Data.FirstOrDefault();
           
            if (foundFav is not null)
            {
                _manager.Delete(foundFav.Id);
                return new SuccessResult("UserFavRemoved");
            }

            return new SuccessResult("UserFavRemoved");
        }
    }
}
