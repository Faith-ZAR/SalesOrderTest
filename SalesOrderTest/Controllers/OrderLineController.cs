using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Data.Models;
using SalesOrder.Services.Services;
using System.Net;
using System.Net.Http;
using System.Xml;

namespace SalesOrderTest.Controllers
{
    [Route("api/orderline")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        private readonly OrderLineService _orderLineService;

        public OrderLineController(OrderLineService orderLineService)
        {
            _orderLineService = orderLineService;
        }

        [HttpGet("list/{orderHeaderId}")]
        public IActionResult GetOrderLineList(int orderHeaderId)
        {
            try
            {
                var orderLines = _orderLineService.GetOrderLines(orderHeaderId);
                return Ok(orderLines);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        [HttpGet("/{orderLineId}")]
        public IActionResult GetOrderLine(int orderLineId)
        {
            try
            {
                var orderLine = _orderLineService.GetOrderLine(orderLineId);

                if (orderLine == null)
                    return NotFound();

                return Ok(orderLine);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }
        [HttpPost("add")]
        public IActionResult AddOrderLine([FromBody] OrderLine orderLine)
        {
            try
            {
                var newOrderLine = _orderLineService.AddOrderLine(orderLine);
                return Ok(newOrderLine);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        [HttpPut("edit/{orderLineId}")]
        public IActionResult UpdateOrderLine([FromBody] OrderLine orderLine)
        {
            try
            {
                var updatedOrderLine = _orderLineService.UpdateOrderLine(orderLine);
                return Ok(updatedOrderLine);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        [HttpDelete("delete/{orderLineId}")]
        public IActionResult DeleteOrderLine([FromBody] OrderLine orderLine)
        {
            try
            {
                _orderLineService.DeleteOrderLine(orderLine);
                return Ok("Successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}");
            }
        }


    }
}
