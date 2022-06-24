using MoneyTrack.Web.Api.Models.Entities;

namespace MoneyTrack.Web.Api.Models.Responses
{
    public class SignInResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
        public UserModel User { get; set; }
    }
}
