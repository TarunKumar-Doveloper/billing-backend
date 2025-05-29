using System.ComponentModel.DataAnnotations;

namespace billing_backend.ViewModel.AuthViewModels
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Please enter a current password.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please enter a new password.")]
        public string NewPassword { get; set; }
    }
}
