using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.Data.LiteDB
{
    static class FilterProcessorExtentions
    {
        public static string ToBsonExpression(this Filter filter)
        {
            var result = $"{filter.PropName} {MapOperations[filter.Operation]} {filter.Value}";

            return result;
        }

        private static Dictionary<Operations, string> MapOperations => new Dictionary<Operations, string>()
        {
            [Operations.Eq] = "=",
            [Operations.NotEq] = "!=",
            [Operations.EqOrGreater] = ">=",
            [Operations.EqOrLess] = "<=",
            [Operations.Greater] = ">",
            [Operations.Less] = "<"
        };
    }
}
