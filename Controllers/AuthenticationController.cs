using billing_backend.Interfaces;
using billing_backend.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace billing_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthenticationController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("User-Login")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequestVM request)
        {
            var result = await _authRepository.UserLogin(request);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpPost("Verify-Login-Otp")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyLoginOtp([FromBody] VerifyOTPVM entity)
        {
            var result = await _authRepository.VerifyLoginOtp(entity);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }
    }
}
