using System.ComponentModel.DataAnnotations;

namespace billing_backend.ViewModel.AuthViewModels
{
    public class LoginResponseVM
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
        public string? AuthToken { get; set; }
        public int ExpiredIn { get; set; }
    }

    public class VerifyOTPVM
    {
        private string _email;

        [Required(ErrorMessage = "Please enter a valid email address.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address.")]
        public string Email
        {
            get { return _email; }
            set { _email = value.ToLower().Trim(); }
        }

        [Required(ErrorMessage = "Please enter the OTP.")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "OTP must be a 6-digit number.")]
        public string? OTP { get; set; }
    }
}
