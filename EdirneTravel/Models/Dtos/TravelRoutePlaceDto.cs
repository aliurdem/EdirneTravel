using EdirneTravel.Models.Dtos.Base;

namespace EdirneTravel.Models.Dtos
{
    public class TravelRoutePlaceDto : BaseDto
    {
        public int Sequence { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
