using EdirneTravel.Application.Services.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Dtos.TravelRoute;
using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using EdirneTravel.Models.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace EdirneTravel.Application.Services
{
    public interface ITravelRouteService : IService<TravelRoute,TravelRouteDto>
    {
        IDataResult<TravelRoute> SaveTravelRouteWithPlaces(TravelRouteDto travelRouteDto);
        IDataResult<TravelRouteDto> GetTravelRouteWithPlacesById(int id);
        IDataResult<PagedList<TravelRouteDto>> GetListWithPlaces([FromQuery] PaginationParameters paginationParameters, [FromBody] FilterParameters filterParameters);
    }
}
