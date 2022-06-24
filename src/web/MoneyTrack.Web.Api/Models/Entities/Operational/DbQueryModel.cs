using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoneyTrack.Web.Api.Models.Entities.Operational
{
    public class DbQueryModel
    {
        public List<FilterModel>? Filters { get; set; }
        public SortingModel? Sorting { get; set; }
        public PagingModel? Paging { get; set; }
    }
}
