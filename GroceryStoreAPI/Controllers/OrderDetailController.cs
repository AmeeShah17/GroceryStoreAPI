using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private OrderDetailRepository _orderdetailRepository;

        public OrderDetailController(OrderDetailRepository orderdetailRepository)
        {
            _orderdetailRepository = orderdetailRepository;
        }
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var orderdetail = _orderdetailRepository.SelectAll();
            return Ok(orderdetail);
        }
        #endregion

        #region GetbyID

        [HttpGet("{OrderDetailID}")]
        public IActionResult GetbyID(int OrderDetailID)
        {
            var orderdetail = _orderdetailRepository.GetbyID(OrderDetailID);
            return Ok(orderdetail);
        }
        #endregion

        #region Delete

        [HttpDelete("{OrderDetailID}")]
        public IActionResult Delete(int OrderDetailID)
        {
            var orderdetail = _orderdetailRepository.OrderDetailDelete(OrderDetailID);
            if (!orderdetail)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Insert

        [HttpPost]
        public IActionResult Add(OrderDetailModel orderDetail)
        {
            if (orderDetail == null)
            {
                return BadRequest();
            }
            bool isInserted = _orderdetailRepository.OrderDetailInsert(orderDetail);
            if (isInserted)
            {
                return Ok(new { Message = "Order detail Inserted" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion


        #region Update

        [HttpPut("{OrderDetailID}")]
        public IActionResult Update([FromBody] OrderDetailModel orderDetail, int OrderDetailID)
        {
            if (orderDetail == null || OrderDetailID != orderDetail.OrderDetailID)
            {
                return BadRequest();
            }
            bool isInserted =_orderdetailRepository.OrderdetailUpdate(orderDetail);

            if (isInserted)
            {
                return Ok(new { Message = "Order Detail Updated" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion

        #region OrderDropDown

        [HttpGet("Order")]
        public IActionResult OrderDropDown()
        {
            var order = _orderdetailRepository.OrderDropDown();
            if (!order.Any())
                return NotFound("No Order found.");

            return Ok(order);
        }
        #endregion

        #region CustomerDropDown

        [HttpGet("Customer")]
        public IActionResult CustomerDropDown()
        {
            var customer = _orderdetailRepository.CustomerDropDown();
            if (!customer.Any())
                return NotFound("No Customer found.");

            return Ok(customer);
        }
        #endregion
    }
}
