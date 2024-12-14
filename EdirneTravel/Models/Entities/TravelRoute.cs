using EdirneTravel.Models.Entities.Base;

namespace EdirneTravel.Models.Entities
{
    public class TravelRoute : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public ICollection<TravelRoutePlace> TravelRoutePlaces { get; set; }
    }
}
