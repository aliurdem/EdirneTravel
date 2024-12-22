using EdirneTravel.Models.Dtos.Base;

namespace EdirneTravel.Models.Dtos
{
    public class PlaceDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VisitableHours { get; set; }
        public int EntranceFee { get; set; }
        public byte[] ImageData { get; set; }
    }
}
