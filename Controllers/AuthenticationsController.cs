using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.Services;

namespace TrackingVoucher_v02.Controllers
{
    [Route("api/v1/auths")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _ser;

        public AuthenticationsController(IAuthenticationService ser)
        {
            _ser = ser;
        }

        #region Authentication APIs
        [HttpPost("no-verify")]
        public ActionResult<User> PostAuthenticationNoVerifyUID(UserCreateRequest entity)
        {
            var user = _ser.AuthenticationNoVerify(entity);
            if (user == null)
            {
                return Problem("Server is maintain. Please try later");
            }
            return Ok(user);
        }

        [HttpPost("verify")]
        public ActionResult<User> PostAuthentication(UserCreateRequest entity)
        {
            var user = _ser.Authentication(entity);
            if (user == null)
            {
                return Problem("Server is maintain. Please try later");
            }
            return Ok(user);
        }
        #endregion
    }
}
