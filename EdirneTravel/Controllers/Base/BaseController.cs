using Azure;
using EdirneTravel.Application.Services.Base;
using EdirneTravel.Models.Entities.Base;
using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using EdirneTravel.Models.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EdirneTravel.Controllers.Base
{
    public class BaseController<TEntity, TDto> : ControllerBase where TEntity : BaseEntity where TDto : IDto
    {
        protected readonly IService<TEntity, TDto> _manager;

        public BaseController(IService<TEntity, TDto> manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TDto model, CancellationToken cancellationToken)
        {
            var result = _manager.Insert(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPatch]
        public virtual async Task<IActionResult> Update([FromBody] TDto model, CancellationToken cancellationToken)
        {
            var result = _manager.Update(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public virtual IAppResult Delete(int id)
        {
            _manager.Delete(id);
            return new SuccessResult("ENTITY_DELETED");
        }


        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var result = _manager.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("[action]")]
        public virtual IActionResult GetAll()
        {
            var result = _manager.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetList")]
        public virtual IActionResult GetList([FromQuery] PaginationParameters paginationParameters, [FromBody] FilterParameters filterParameters)
        {
            var result = _manager.GetList(paginationParameters, filterParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.Data.MetaData));

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }


    }
}
