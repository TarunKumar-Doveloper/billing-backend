﻿namespace billing_backend.Helper
{
    public static class ResponseMessage
    {
        public static string IncorrectEmail = "Invalid email address. Please enter a valid email.";
        public static string IncorrectPassword = "Incorrect password. Please try again.";
        public static string InactiveUser = "Your account is inactive. Please contact the administrator.";
        public static string OtpNotGenerated = "OTP was not sent. Please try again.";
        public static string OtpSend = "OTP send Successfully";
        public static string AccountNotFound = "Account not found. Please check your credentials.";
        public static string IncorrectOtp = "Incorrect OTP entered. Please try again.";
        public static string OtpExpired = "The OTP has expired. Please request a new OTP.";
        public static string TokenGenerationFailed = "Failed to generate authentication token. Please try again.";
        public static string VerifyOTP = "OTP verified successfully.";
        public static string PasswordChange = "Password changed successfully.";
        public static string ForgotPassword = "Forget password successfully.";


        //Common messages
        public static string DateRetrived = "Data retrived successfully.";
        public static string DataNotFound = "Data not found.";


        // Product related messages
        public static string ProductAdded = "Product added successfully.";
        public static string ProductUpdated = "Product updated successfully.";
        public static string ProductDeleted = "Product deleted successfully.";
        public static string ProductNotFound = "Product not found.";
        
    }
}
