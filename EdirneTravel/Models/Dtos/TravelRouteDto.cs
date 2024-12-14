using EdirneTravel.Models.Dtos.Base;
using EdirneTravel.Models.Entities;

namespace EdirneTravel.Models.Dtos
{
    public class TravelRouteDto : BaseDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public List<TravelRoutePlaceDto>? Places { get; set; }
    }
}
