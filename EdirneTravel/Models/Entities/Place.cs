using EdirneTravel.Models.Entities.Base;

namespace EdirneTravel.Models.Entities
{
    public class Place : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LocationInfo { get; set; }
        public string VisitableHours { get; set; }
        public int EntranceFee { get; set; }
        public byte[] ImageData { get; set; }

        public ICollection<UserFav> UserFavs { get; set; }
        public ICollection<TravelRoutePlace> RoutePlaces { get; set; }
    }
}
