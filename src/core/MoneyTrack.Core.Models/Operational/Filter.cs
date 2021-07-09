using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.Models.Operational
{
    public class Filter
    {
        public string PropName { get; set; }
        public string Value { get; set; }
        public Operations Operations { get; set; }
    }

    public enum Operations
    {
        Eq, 
        NotEq, 
        Less, 
        EqOrLess, 
        Greater, 
        EqOrGreater,
        Like
    }
}
