using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        #region GetAll

        [HttpGet]
        public IActionResult GetAll()
        {
            var order = _orderRepository.SelectAll();
            return Ok(order);
        }
        #endregion

        #region GetbyID

        [HttpGet("{OrderID}")]
        public IActionResult GetbyID(int OrderID)
        {
            var order = _orderRepository.GetbyID(OrderID);
            return Ok(order);
        }
        #endregion

        #region Delete

        [HttpDelete("{OrderID}")]
        public IActionResult Delete(int OrderID)
        {
            var order = _orderRepository.OrderDelete(OrderID);
            if (!order)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region insert

        [HttpPost]
        public IActionResult Add(OrderModel order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            bool isInserted = _orderRepository.OrderInsert(order);
            if (isInserted)
            {
                return Ok(new { Message = "Order Inserted" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion


        #region Update

        [HttpPut("{OrderID}")]
        public IActionResult Update([FromBody] OrderModel order, int OrderID)
        {
            if (order == null || OrderID != order.OrderID)
            {
                return BadRequest();
            }
            bool isInserted = _orderRepository.OrderUpdate(order);

            if (isInserted)
            {
                return Ok(new { Message = "Order Updated" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion

        #region CustomerDropdown

        [HttpGet("Customer")]
        public IActionResult CustomerDropDown()
        {
            var customer = _orderRepository.CustomerDropDown();
            if (!customer.Any())
                return NotFound("No Customer found.");

            return Ok(customer);
        }
        #endregion
    }
}
