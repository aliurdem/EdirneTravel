using EdirneTravel.Models.Entities.Base;

namespace EdirneTravel.Models.Entities
{
    public class UserFav : BaseEntity
    {
        public int PlaceId { get; set; }
        public string UserId { get; set; }

        // Navigation properties
        public Place Place { get; set; }
        public User User { get; set; }
    }
}
