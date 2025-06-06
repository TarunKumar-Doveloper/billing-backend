﻿using billing_backend.DataContext;
using billing_backend.Helper;
using billing_backend.Interfaces;
using billing_backend.Models;
using billing_backend.ViewModel;
using billing_backend.ViewModel.AuthViewModels;
using Microsoft.EntityFrameworkCore;

namespace billing_backend.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly TokenService _tokenService;

        public AuthRepository(AppDbContext dbContext, TokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<BaseResponse> UserLogin(LoginRequestVM entity)
        {
            try
            {
                var user = await _dbContext.UserMaster.FirstOrDefaultAsync(x => x.Email.ToLower() == entity.Email.ToLower());

                if (user == null)
                    return new BaseResponse { Success = false, Message = ResponseMessage.IncorrectEmail };

                bool IsUserCredentialsValid = user.Password == entity.Password;

                if (!IsUserCredentialsValid)
                    return new BaseResponse { Success = false, Message = ResponseMessage.IncorrectPassword };

                if (!user.IsActive)
                    return new BaseResponse { Success = false, Message = ResponseMessage.InactiveUser };

                var otpGenrated = await GenerateOtp(user.Email);

                if (otpGenrated == null)
                    return new BaseResponse { Success = false, Message = ResponseMessage.OtpNotGenerated };

                return new BaseResponse { Success = true, Message = ResponseMessage.OtpSend };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponseObjectModel<LoginResponseVM>> VerifyLoginOtp(VerifyOTPVM entity)
        {
            try
            {
                var user = await _dbContext.UserMaster.FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == entity.Email);

                if (user == null)
                    return new BaseResponseObjectModel<LoginResponseVM>
                    {
                        Success = false,
                        Message = ResponseMessage.AccountNotFound
                    };

                var otpExits = await _dbContext.OtpMaster.FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (otpExits.OtpCode != entity.OTP)
                    return new BaseResponseObjectModel<LoginResponseVM>
                    {
                        Success = false,
                        Message = ResponseMessage.IncorrectOtp
                    };

                DateTime currentTime = DateTime.UtcNow;
                DateTime otpExpiryTime = (DateTime)otpExits.ExpiryTime;

                if (currentTime > otpExpiryTime)
                    return new BaseResponseObjectModel<LoginResponseVM>
                    {
                        Success = false,
                        Message = ResponseMessage.OtpExpired
                    };

                var token = _tokenService.GenerateJwtToken(user);

                if (token == null)
                    return new BaseResponseObjectModel<LoginResponseVM>
                    {
                        Success = false,
                        Message = ResponseMessage.TokenGenerationFailed
                    };

                var tokenVM = new LoginResponseVM()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = "",
                    IsActive = user.IsActive,
                    AuthToken = token,
                    ExpiredIn = 300,
                };

                otpExits.IsUsed = true;

                await _dbContext.SaveChangesAsync();

                return new BaseResponseObjectModel<LoginResponseVM>
                {
                    Success = true,
                    Message = ResponseMessage.VerifyOTP,
                    Data = tokenVM
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> ResendLoginOtp(string Email)
        {
            try
            {
                var user = await _dbContext.UserMaster.FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == Email.ToLower().Trim());

                if (user == null)
                    return new BaseResponse { Success = false, Message = ResponseMessage.AccountNotFound };

                var Success = await GenerateOtp(user.Email);

                if (Success == null)
                    return new BaseResponse { Success = false, Message = ResponseMessage.OtpNotGenerated };

                return new BaseResponse { Success = true, Message = ResponseMessage.OtpSend };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> ChangePassword(Guid LoggedInUserId, ChangePasswordVM entity)
        {
            try
            {
                var user = await _dbContext.UserMaster.FirstOrDefaultAsync(x => x.Id == LoggedInUserId);

                if (user == null)
                    return new BaseResponse { Success = false, Message = ResponseMessage.AccountNotFound };

                UserMaster ExistingDetail = user;

                if (user.Password == entity.CurrentPassword)
                {
                    user.Password = entity.NewPassword;

                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return new BaseResponse { Success = false, Message = ResponseMessage.IncorrectPassword };
                }
                return new BaseResponse { Success = true, Message = ResponseMessage.PasswordChange };
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> ForgetPassword(Guid LoggedInUserId, ForgetPasswordVM entity)
        {
            try
            {
                var user = await _dbContext.UserMaster.FirstOrDefaultAsync(x => x.Id == LoggedInUserId);

                if (user == null)
                    return new BaseResponse { Success = false, Message = ResponseMessage.AccountNotFound };

                user.Password = entity.NewPassword;

                await _dbContext.SaveChangesAsync();

                return new BaseResponse { Success = true, Message = ResponseMessage.ForgotPassword };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> EmailSendOtp(SentOtpEmailVM entity)
        {
            try
            {
                var user = await _dbContext.UserMaster.FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == entity.Email.ToLower().Trim());

                if (user == null)
                    return new BaseResponse { Success = false, Message = ResponseMessage.AccountNotFound };

                if (!user.IsActive)
                    return new BaseResponse { Success = false, Message = ResponseMessage.InactiveUser };

                var Success = await GenerateOtp(user.Email);

                return new BaseResponse { Success = true, Message = ResponseMessage.OtpSend };

            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Common Methods

        public async Task<string> GenerateOtp(string Email)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            var userDetail = await _dbContext.UserMaster.FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == Email.ToLower().Trim());

            var existingOtp = await _dbContext.OtpMaster.FirstOrDefaultAsync(x => x.UserId == userDetail.Id);

            if (existingOtp != null)
            {
                existingOtp.OtpCode = otp;
                existingOtp.ExpiryTime = DateTime.UtcNow.AddMinutes(5);
                existingOtp.IsUsed = false;

                _dbContext.OtpMaster.Update(existingOtp);
            }
            else
            {
                var otpEntry = new OtpMaster
                {
                    Id = Guid.NewGuid(),
                    UserId = userDetail.Id,
                    OtpCode = otp,
                    ExpiryTime = DateTime.UtcNow.AddMinutes(5),
                    IsUsed = false
                };

                await _dbContext.OtpMaster.AddAsync(otpEntry);
            }

            await _dbContext.SaveChangesAsync();

            return otp;
        }

        #endregion
    }
}
