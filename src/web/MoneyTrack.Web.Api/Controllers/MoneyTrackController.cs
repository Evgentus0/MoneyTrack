using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTrack.Core.DomainServices.Exceptions;
using System.Security.Claims;

namespace MoneyTrack.Web.Api.Controllers
{
    public class MoneyTrackController : ControllerBase
    {
        protected string GetCurrentUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new MoneyTrackException("Can not find name indentifier in claims");

            return userId;
        }
    }
}
