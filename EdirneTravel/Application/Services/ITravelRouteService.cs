using EdirneTravel.Application.Services.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Dtos.TravelRoute;
using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Utilities.Results;

namespace EdirneTravel.Application.Services
{
    public interface ITravelRouteService : IService<TravelRoute,TravelRouteDto>
    {
        IDataResult<TravelRoute> SaveTravelRouteWithPlaces(TravelRouteDto travelRouteDto);
        IDataResult<TravelRouteDto> GetTravelRouteWithPlacesById(int id);
    }
}
