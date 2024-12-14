using EdirneTravel.Models.Entities.Base;

namespace EdirneTravel.Models.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<TravelRoute> Routes { get; set; }

    }
}
