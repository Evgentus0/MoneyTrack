using System.ComponentModel.DataAnnotations;

namespace MoneyTrack.Web.Api.Models.Entities
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsSystem { get; set; }
    }
}
