using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("addProduct")]
        [ProducesResponseType(typeof(ProductDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDTO>> AddProduct(ProductDTO productDTO)
        {
            try
            {
                var result = await _productService.AddProduct(productDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }


        [HttpGet("getAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }
        [HttpGet("getAvailableProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAvailableProducts()
        {
            try
            {
                var products = await _productService.GetAllAvailableProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("getById")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("getByCategory")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(string category)
        {
            try
            {
                var products = await _productService.GetProductsByCategory(category);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [HttpPut("updatePrice")]
        [ProducesResponseType(typeof(ProductPriceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ProductPriceDTO>> UpdateProductPrice(ProductPriceDTO productPriceDTO)
        {
            try
            {
                var result = await _productService.UpdatePrice(productPriceDTO);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Conflict(new ErrorModel(409, ex.Message));
            }
        }

        [HttpPut("updateStatus")]
        [ProducesResponseType(typeof(ProductStatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ProductStatusDTO>> UpdateProductStatus(ProductStatusDTO productStatusDTO)
        {
            try
            {
                var result = await _productService.UpdateProductStatus(productStatusDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new ErrorModel(409, ex.Message));
            }
        }

        [HttpPut("updateStock")]
        [ProducesResponseType(typeof(ProductStockDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ProductStockDTO>> UpdateProductStock(ProductStockDTO productStock)
        {
            try
            {
                var result = await _productService.UpdateProductStock(productStock);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new ErrorModel(409, ex.Message));
            }
        }
    }
}
