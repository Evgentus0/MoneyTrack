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

            var result = $"{propName} {MapOperations[filter.Operation]} {string.Format(ValueTemplate[filter.Operation], filter.Value)}";

            return result;
        }

        private static Dictionary<Operations, string> MapOperations => new Dictionary<Operations, string>()
        {
            [Operations.Eq] = "=", [Operations.EqString] = "=",
            [Operations.NotEq] = "!=", [Operations.NotEqString] = "!=",
            [Operations.EqOrGreater] = ">=",
            [Operations.EqOrLess] = "<=",
            [Operations.Greater] = ">",
            [Operations.Less] = "<",
            [Operations.Like] = "like", [Operations.StartWith] = "like", [Operations.EndWith] = "like"
        };

        private static Dictionary<Operations, string> ValueTemplate => new Dictionary<Operations, string>()
        {
            [Operations.Eq] = "{0}", [Operations.NotEq] = "{0}", 
            [Operations.EqOrGreater] = "{0}", [Operations.EqOrLess] = "{0}",
            [Operations.Greater] = "{0}", [Operations.Less] = "{0}",

            [Operations.EqString] = "\'{0}\'", [Operations.NotEqString] = "\'{0}\'",
            [Operations.Like] = "\'%{0}%\'",
            [Operations.StartWith] = "\'{0}%\'",
            [Operations.EndWith] = "\'%{0}\'",
        };
    }
}
