using EdirneTravel.Models.Entities.Base;

namespace EdirneTravel.Models.Entities
{
    public class TravelRoutePlace : BaseEntity
    {
        public int PlaceId { get; set; }
        public int TravelRouteId { get; set; }
        public int Sequence { get; set; }

        // Navigation properties
        public Place Place { get; set; }
        public TravelRoute TravelRoute { get; set; }
    }
}
