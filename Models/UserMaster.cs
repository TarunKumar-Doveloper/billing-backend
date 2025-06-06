﻿using System.ComponentModel.DataAnnotations;

namespace billing_backend.Models
{
    public class UserMaster
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
