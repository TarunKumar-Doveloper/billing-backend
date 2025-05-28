using System.ComponentModel.DataAnnotations;

namespace billing_backend.Models
{
    public class CustomerMaster
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
