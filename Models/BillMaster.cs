using System.ComponentModel.DataAnnotations.Schema;

namespace billing_backend.Models
{
    public class BillMaster
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerMaster Customer { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
