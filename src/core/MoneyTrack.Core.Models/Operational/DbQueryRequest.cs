using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.Models.Operational
{
    public class DbQueryRequest
    {
        public List<Filter> Filters { get; set; }
        public Sorting Sorting { get; set; }
        public Paging Paging { get; set; }
    }
}
