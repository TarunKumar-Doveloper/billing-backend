using billing_backend.Interfaces;
using billing_backend.ViewModel.ProductViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace billing_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("Get-Product-By-Barcode/{barcode}")]
        public async Task<IActionResult> GetProductByBarcode(string barcode)
        {
            var result = await _productRepository.GetProductByBarcode(barcode);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpGet("Get-Product-ById/{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var result = await _productRepository.GetProductById(productId);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpGet("Get-All-Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productRepository.GetAllProducts();

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpPost("Add-Update-Product")]
        public async Task<IActionResult> AddOrUpdateProduct([FromBody] AddUpdateProductVM entity)
        {
            var result = await _productRepository.AddOrUpdateProduct(LoggedInUserId, entity);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpDelete("Delete-Product/{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var result = await _productRepository.DeleteProduct(productId);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }
    }
}
