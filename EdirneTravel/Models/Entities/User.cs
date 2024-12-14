
using Microsoft.AspNetCore.Identity;

namespace EdirneTravel.Models.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserFav> UserFavs { get; set; }
        public ICollection<TravelRoute> Routes { get; set; }
    }
}
