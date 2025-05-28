using System.ComponentModel.DataAnnotations;

namespace billing_backend.Models
{
    public class OtpMaster
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? OtpCode { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; }
    }
}
