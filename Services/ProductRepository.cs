using billing_backend.DataContext;
using billing_backend.Helper;
using billing_backend.Interfaces;
using billing_backend.Models;
using billing_backend.ViewModel.ProductViewModels;
using Microsoft.EntityFrameworkCore;

namespace billing_backend.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        
        public ProductRepository(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<BaseResponseObjectModel<ProductMaster>> GetProductByBarcode(string barcode)
        {
            try
            {
                var result = await _dbContext.ProductMaster.FirstOrDefaultAsync(x => x.Barcode == barcode && x.IsActive);

                return new BaseResponseObjectModel<ProductMaster>
                {
                    Success = result != null,
                    Message = result != null ? ResponseMessage.DateRetrived : ResponseMessage.ProductNotFound,
                    Data = result
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponseObjectModel<ProductMaster>> GetProductById(Guid productId)
        {
            try
            {
                var result = await _dbContext.ProductMaster.FirstOrDefaultAsync(p => p.Id == productId && p.IsActive);

                return new BaseResponseObjectModel<ProductMaster>
                {
                    Success = result != null,
                    Message = result != null ? ResponseMessage.DateRetrived : ResponseMessage.ProductNotFound,
                    Data = result
                };
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponseModel<IEnumerable<ProductMaster>>> GetAllProducts()
        {
            try
            {

                var result = await _dbContext.ProductMaster.Where(p => p.IsActive).ToListAsync();

                return new BaseResponseModel<IEnumerable<ProductMaster>>
                {
                    Success = result.Any(),
                    Message = result.Any() ? ResponseMessage.DateRetrived : ResponseMessage.DataNotFound,
                    Data = result
                };
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> AddOrUpdateProduct(Guid LoggedInUserId, AddUpdateProductVM entity)
        {
            try
            {
                var product = await _dbContext.ProductMaster.FirstOrDefaultAsync(p => p.Id == entity.Id && p.IsActive);

                if(product == null)
                {
                    var newProduct = new ProductMaster
                    {
                        Id = Guid.NewGuid(),
                        Barcode = entity.Barcode,
                        ProductName = entity.ProductName,
                        Price = entity.Price,
                        GSTPercentage = entity.GSTPercentage,
                        IsActive = true,
                        CreatedBy = LoggedInUserId
                    };

                    await _dbContext.ProductMaster.AddAsync(newProduct);
                }
                else
                {
                    product.Barcode = entity.Barcode;
                    product.ProductName = entity.ProductName;
                    product.Price = entity.Price;
                    product.GSTPercentage = entity.GSTPercentage;
                }

                await _dbContext.SaveChangesAsync();

                return new BaseResponse
                {
                    Success = true,
                    Message = product == null ? ResponseMessage.ProductAdded : ResponseMessage.ProductUpdated,
                };
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> DeleteProduct(Guid productId)
        {
            try
            {
                var product = await _dbContext.ProductMaster.FindAsync(productId);
                
                if (product == null || !product.IsActive)
                    return new BaseResponse
                    {
                        Success = false,
                        Message = ResponseMessage.ProductNotFound
                    };

                product.IsActive = false;

                await _dbContext.SaveChangesAsync();

                return new BaseResponse
                {
                    Success = true,
                    Message = ResponseMessage.ProductDeleted
                };
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
    