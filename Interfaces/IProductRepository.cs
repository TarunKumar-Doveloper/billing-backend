using billing_backend.Helper;
using billing_backend.Models;
using billing_backend.ViewModel.ProductViewModels;

namespace billing_backend.Interfaces
{
    public interface IProductRepository
    {
        Task<BaseResponseObjectModel<ProductMaster>> GetProductByBarcode(string barcode);

        Task<BaseResponseObjectModel<ProductMaster>> GetProductById(Guid productId);

        Task<BaseResponseModel<IEnumerable<ProductMaster>>> GetAllProducts();

        Task<BaseResponse> AddOrUpdateProduct(Guid LoggedInUserId, AddUpdateProductVM entity);

        Task<BaseResponse> DeleteProduct(Guid productId);
    }
}
