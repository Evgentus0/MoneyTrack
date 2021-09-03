namespace MoneyTrack.Core.Models.Operational
{
    public class Sorting
    {
        public string PropName { get; set; }
        public SortDirect Direction { get; set; }
    }
    public enum SortDirect
    {
        Asc = 0,
        Desc = 1
    }
}
