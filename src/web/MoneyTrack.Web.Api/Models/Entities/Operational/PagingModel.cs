namespace MoneyTrack.Web.Api.Models.Entities.Operational
{
    public class PagingModel
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
