﻿using System.ComponentModel.DataAnnotations;

namespace billing_backend.ViewModel.AuthViewModels
{
    public class LoginRequestVM
    {
        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
