using System;
using System.Collections.Generic;

namespace MoneyTrack.Core.Models.Operational
{
    public class Filter
    {
        public string PropName { get; set; }
        public string Value { get; set; }
        public Operations Operation { get; set; }
        public FilterOp FilterOp { get; set; }

        public static List<Operations> GetAvailableOperations(Type type)
        {
            var result = new List<Operations>();

            if(type == typeof(string))
            {
                result = new List<Operations> { Operations.EqString, Operations.NotEqString,
                Operations.Like, Operations.StartWith, Operations.EndWith };
            }
            else if(type == typeof(int) || type == typeof(int?)
                 || type == typeof(double) || type == typeof(double?)
                 || type == typeof(float) || type == typeof(float?)
                 || type == typeof(decimal) || type == typeof(decimal?)
                 || type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?)
                 || type == typeof(DateTime) || type == typeof(DateTime?))
            {
                result = new List<Operations> { Operations.Eq, Operations.NotEq, Operations.Less, Operations.EqOrLess,
                Operations.Greater, Operations.EqOrGreater };
            }

            return result;
        }
    }

    public enum Operations
    {
        // Number
        Eq = 1,
        NotEq = 2,
        Less = 3, 
        EqOrLess = 4, 
        Greater = 5, 
        EqOrGreater = 6,

        // String
        EqString = 101,
        NotEqString = 102,
        Like = 103,
        StartWith = 104,
        EndWith = 105
    }

    public enum FilterOp
    {
        And = 1, 
        Or = 2
    }
}
