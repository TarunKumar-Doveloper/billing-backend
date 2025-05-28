using billing_backend.Helper;
using billing_backend.ViewModel;
using Microsoft.AspNetCore.Identity.Data;

namespace billing_backend.Interfaces
{
    public interface IAuthRepository
    {
        Task<BaseResponse> UserLogin(LoginRequestVM entity);

        Task<BaseResponseObjectModel<LoginResponseVM>> VerifyLoginOtp(VerifyOTPVM entity);

        Task<BaseResponse> ResendLoginOtp(string Email);

        Task<BaseResponse> ChangePassword(Guid LoggedInUserId, ChangePasswordVM entity);

        Task<BaseResponse> EmailSendOtp(SentOtpEmailVM entity);

        Task<BaseResponse> ForgetPassword(Guid LoggedInUserId, ForgetPasswordVM entity);
    }
}
