using billing_backend.Interfaces;
using billing_backend.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace billing_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : BaseController
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

        [HttpPost("ReSend-Otp-Email/{Email}")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendLoginOtp(string Email)
        {
            var result = await _authRepository.ResendLoginOtp(Email);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpPut("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM entity)
        {
            var result = await _authRepository.ChangePassword(LoggedInUserId, entity);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpPut("Forgot-Password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordVM entity)
        {
            var result = await _authRepository.ForgetPassword(LoggedInUserId, entity);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpPost("SendOtp-Email")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPasswordEmailSendOtp([FromBody] SentOtpEmailVM entity)
        {
            var result = await _authRepository.EmailSendOtp(entity);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }
    }
}
