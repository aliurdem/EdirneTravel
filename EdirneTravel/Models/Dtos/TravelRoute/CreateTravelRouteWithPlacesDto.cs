using EdirneTravel.Models.Dtos.Base;

namespace EdirneTravel.Models.Dtos.TravelRoute
{
    public class CreateTravelRouteWithPlacesDto : BaseDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public List<CreateTravelRoutePlaceDto> Places { get; set; }

    }
    public class CreateTravelRoutePlaceDto()
    {
        public int PlaceId { get; set; }
        public int Sequence { get; set; }
    }
}
