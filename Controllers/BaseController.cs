using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace billing_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private protected Guid _loggedInUserId;

        protected Guid LoggedInUserId
        {
            get
            {
                return GetLoggedInUserId();
            }
        }
        protected Guid GetLoggedInUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.PrimarySid);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid parsedUserId))
            {
                throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
            }

            return parsedUserId;
        }
    }
}
