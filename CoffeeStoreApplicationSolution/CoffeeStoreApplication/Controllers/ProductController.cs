using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        
        [Authorize(Roles = "Manager,Barista")]
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
                _logger.LogError(ex.Message, ex);
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [Authorize(Roles = "Manager,Barista")]
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
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Manager,Barista")]
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
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Manager,Barista")]
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
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Manager,Barista")]
        [HttpGet("getByName")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProductByName(string name)
        {
            try
            {
                var product = await _productService.GetByName(name);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Manager,Barista")]
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
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("updatePrice")]
        [ProducesResponseType(typeof(ProductPriceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductPriceDTO>> UpdateProductPrice(ProductPriceDTO productPriceDTO)
        {
            try
            {
                var result = await _productService.UpdatePrice(productPriceDTO);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(409, ex.Message));
            }
        }

        [Authorize(Roles = "Manager,Barista")]
        [HttpPut("updateStatus")]
        [ProducesResponseType(typeof(ProductStatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductStatusDTO>> UpdateProductStatus(ProductStatusDTO productStatusDTO)
        {
            try
            {
                var result = await _productService.UpdateProductStatus(productStatusDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(409, ex.Message));
            }
        }

        [Authorize(Roles = "Manager,Barista")]
        [HttpPut("updateStock")]
        [ProducesResponseType(typeof(ProductStockDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductStockDTO>> UpdateProductStock(ProductStockDTO productStock)
        {
            try
            {
                var result = await _productService.UpdateProductStock(productStock);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(409, ex.Message));
            }
        }
    }
}
