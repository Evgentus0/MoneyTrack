using MoneyTrack.Core.Models.Operational;

namespace MoneyTrack.Web.Api.Models.Entities.Operational
{
    public class SortingModel
    {
        public string PropName { get; set; }
        public SortDirect Direction { get; set; }
    }
}
