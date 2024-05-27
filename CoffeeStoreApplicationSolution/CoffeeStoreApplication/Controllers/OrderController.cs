using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.CustomerOrder;
using CoffeeStoreApplication.Models.DTOs.Order;
using CoffeeStoreApplication.Models.DTOs.OrderItems;
using CoffeeStoreApplication.Models.DTOs.Product;
using CoffeeStoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderService _orderService;
        private readonly ICustomerOrderService _customerOrderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ICustomerOrderService customerOrderService, IOrderItemService orderItemService, ILogger<OrderController> logger)
        {
            _orderItemService = orderItemService;
            _orderService = orderService;
            _customerOrderService = customerOrderService;
            _logger = logger;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("addOrder")]
        [ProducesResponseType(typeof(OrderReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderReturnDTO>> AddProduct(OrderDTO orderDTO)
        {
            try
            {
                var result = await _orderService.AddOrder(orderDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("cancelOrder")]
        [ProducesResponseType(typeof(OrderReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderReturnDTO>> CancelOrder([FromBody] int orderId)
        {
            try
            {
                var result = await _orderService.CancelOrder(orderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Barista")]
        [HttpGet("pendingOrders")]
        [ProducesResponseType(typeof(IEnumerable<OrderReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderReturnDTO>>> GetAllPendingOrders()
        {
            try
            {
                var result = await _orderService.GetAllPendingOrders();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Barista")]
        [HttpPut("updateOrderStatus")]
        [ProducesResponseType(typeof(OrderStatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status304NotModified)]
        public async Task<ActionResult<OrderStatusDTO>> UpdateStatus(OrderStatusDTO orderStatusDTO)
        {
            try
            {
                var result = await _orderService.UpdateOrderStatus(orderStatusDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Conflict(new ErrorModel(304, ex.Message));
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("getOrderByCustomerId")]
        [ProducesResponseType(typeof(IEnumerable<CustomerOrderReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerOrderReturnDTO>>> GetOrdersByCustomerId(int customerId)
        {
            try
            {
                var result = await _customerOrderService.GetCustomerOrderById(customerId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Barista")]
        [HttpGet("getOrderItemsByOrderId")]
        [ProducesResponseType(typeof(IEnumerable<OrderItemDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItemsByOrderId(int orderId)
        {
            try
            {
                var result = await _orderItemService.GetOrderItemsByOrderId(orderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }
    }
}
