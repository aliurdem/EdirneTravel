using EdirneTravel.Models.Dtos.Base;

namespace EdirneTravel.Models.Dtos
{
    public class UserFavDto : BaseDto
    {
        public int PlaceId { get; set; }
        public string UserId { get; set; }
    }
}
