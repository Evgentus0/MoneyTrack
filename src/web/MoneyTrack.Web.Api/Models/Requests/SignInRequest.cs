using System.ComponentModel.DataAnnotations;

namespace MoneyTrack.Web.Api.Models.Requests
{
    public class SignInRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
