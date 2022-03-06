namespace MoneyTrack.Web.Api.Models.Responses
{
    public class SignInResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
