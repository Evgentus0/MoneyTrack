using System.Collections.Generic;

namespace MoneyTrack.Core.Models.Operational
{
    public class DbQueryRequest
    {
        public List<Filter> Filters { get; set; }
        public Sorting Sorting { get; set; }
        public Paging Paging { get; set; }
    }
}
