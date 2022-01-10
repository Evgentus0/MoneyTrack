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
        Eq,
        NotEq,
        Less, 
        EqOrLess, 
        Greater, 
        EqOrGreater,

        // String
        EqString,
        NotEqString,
        Like,
        StartWith,
        EndWith
    }

    public enum FilterOp
    {
        And, 
        Or
    }
}
