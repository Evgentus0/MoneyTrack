using MoneyTrack.Web.Api.Models.Entities;

namespace MoneyTrack.Web.Api.Models.Requests
{
    public class SignUpRequest
    {
        public UserModel User { get; set; }
        public string Passwords { get; set; }
    }
}
