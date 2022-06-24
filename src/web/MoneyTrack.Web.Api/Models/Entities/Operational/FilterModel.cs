using MoneyTrack.Core.Models.Operational;

namespace MoneyTrack.Web.Api.Models.Entities.Operational
{
    public class FilterModel
    {
        public string PropName { get; set; }
        public string Value { get; set; }
        public Operations Operation { get; set; }
        public FilterOp FilterOp { get; set; }
    }
}
