namespace MoneyTrack.Core.Models.Operational
{
    public class Filter
    {
        public string PropName { get; set; }
        public string Value { get; set; }
        public Operations Operation { get; set; }
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
