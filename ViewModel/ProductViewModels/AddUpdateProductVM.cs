using System.ComponentModel.DataAnnotations.Schema;

namespace billing_backend.ViewModel.ProductViewModels
{
    public class AddUpdateProductVM
    {
        public Guid Id { get; set; }
        public string? Barcode { get; set; }
        public string? ProductName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal GSTPercentage { get; set; }
    }
}
