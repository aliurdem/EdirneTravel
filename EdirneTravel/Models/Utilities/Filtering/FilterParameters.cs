using EdirneTravel.Models.Enums;

namespace EdirneTravel.Models.Utilities.Filtering
{
    public class FilterParameters
    {
        public List<Filter> Filters { get; set; } = new List<Filter>();
        public string OrderProp { get; set; } = "";
        public OrderTypeEnum OrderType { get; set; } = OrderTypeEnum.None;
    }
}
