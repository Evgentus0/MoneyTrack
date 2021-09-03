using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;

namespace MoneyTrack.Core.Data.LiteDB
{
    internal static class FilterProcessorExtentions
    {
        public static string ToBsonExpression(this Filter filter)
        {
            var propName = filter.PropName;
            if(string.Equals("id", filter.PropName, StringComparison.OrdinalIgnoreCase))
            {
                propName = "_id";
            }

            var result = $"{propName} {MapOperations[filter.Operation]} {filter.Value}";

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
