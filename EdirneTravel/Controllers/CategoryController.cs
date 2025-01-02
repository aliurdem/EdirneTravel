using EdirneTravel.Application.Services.Base;
using EdirneTravel.Controllers.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using EdirneTravel.Models.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdirneTravel.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category, CategoryDto>
    {
        private readonly IService<TravelRoute, TravelRouteDto> _travelRouteService;

        public CategoryController(IService<Category, CategoryDto> service, IService<TravelRoute, TravelRouteDto> travelRouteService) : base(service)
        {
            _travelRouteService = travelRouteService;
        }

        [HttpDelete("{id}")]
        public override IAppResult Delete(int id)
        {
            PaginationParameters paginationParameters = new PaginationParameters();
            FilterParameters filterParameters = new FilterParameters();

            Filter filter = new Filter();
            filter.Property = "CategoryId";
            filter.Operator = "==";
            filter.Value = id.ToString();

            filterParameters.Filters.Add(filter);

            var result = _travelRouteService.GetList(paginationParameters, filterParameters);

            if (result.Data.Count() != 0)
            {
                return new ErrorResult("Kategori ilişkili rotalar sebebiyle silinemedi");
            }

            return base.Delete(id);
        }
    }
}
