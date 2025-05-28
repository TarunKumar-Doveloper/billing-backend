using billing_backend.Helper;
using billing_backend.ViewModel;
using Microsoft.AspNetCore.Identity.Data;

namespace billing_backend.Interfaces
{
    public interface IAuthRepository
    {
        Task<BaseResponse> UserLogin(LoginRequestVM entity);

        Task<BaseResponseObjectModel<LoginResponseVM>> VerifyLoginOtp(VerifyOTPVM entity);
    }
}
