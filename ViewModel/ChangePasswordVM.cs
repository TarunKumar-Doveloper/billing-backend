using System.ComponentModel.DataAnnotations;

namespace billing_backend.ViewModel
{
    public class ChangePasswordVM
    {
        private string _emailId;

        [Required(ErrorMessage = "Please enter a valid email address.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress, ErrorMessage = "Please enter a valid email address.")]
        public string EmailId
        {
            get { return _emailId; }
            set { _emailId = value?.ToLower(); } // Convert to lowercase before setting
        }

        [Required(ErrorMessage = "Please enter a current password.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please enter a new password.")]
        public string NewPassword { get; set; }
    }
}
