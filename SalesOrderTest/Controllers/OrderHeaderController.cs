using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Data.Models;
using SalesOrder.Services.Services;
using System.Net;

namespace SalesOrderTest.Controllers
{
    [Route("api/orderheader")]
    [ApiController]
    public class OrderHeaderController : ControllerBase
    {
        private readonly OrderHeaderService _orderHeaderService;

        public OrderHeaderController(OrderHeaderService orderHeaderService)
        {
            _orderHeaderService = orderHeaderService;
        }

        [HttpGet("list")]
        public IActionResult GetAllOrderHeaders()
        {
            try
            {
                var orderHeaders = _orderHeaderService.GetAllOrderHeaders();
                return Ok(orderHeaders);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        [HttpGet("{orderHeaderId}")]
        public IActionResult GetOrderHeader(int orderHeaderId)
        {
            try
            {
                var orderHeader = _orderHeaderService.GetOrderHeader(orderHeaderId);
                if (orderHeader == null)
                    return NotFound();

                return Ok(orderHeader);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public IActionResult AddOrderHeader([FromBody] OrderHeader orderHeader)
        {
            try
            {
                var newOrderHeader = _orderHeaderService.AddOrderHeader(orderHeader);
                return Ok(newOrderHeader);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        [HttpPut("edit/{orderHeaderId}")]
        public IActionResult UpdateOrderHeader([FromBody] OrderHeader orderHeader)
        {
            try
            {
                var updatedOrderHeader = _orderHeaderService.UpdateOrderHeader(orderHeader);
                return Ok(updatedOrderHeader);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        [HttpDelete("delete/{orderHeaderId}")]
        public IActionResult DeleteOrderHeader([FromBody] OrderHeader orderHeader)
        {
            try
            {
                _orderHeaderService.DeleteOrderHeader(orderHeader);
                return Ok("Successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }
    }
}
