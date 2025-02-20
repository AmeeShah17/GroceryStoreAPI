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
    
    public class BillController : ControllerBase
    {
        private BillRepository _billRepository;

        public BillController(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        #region GetAll

        [HttpGet]
        public IActionResult GetAll()
        {
            var bill = _billRepository.SelectAll();
            return Ok(bill);
        }
        #endregion

        #region GetByID

        [HttpGet("{BillID}")]
        public IActionResult GetbyID(int BillID)
        {
            var bill = _billRepository.GetbyID(BillID);
            return Ok(bill);
        }
        #endregion

        #region Delete

        [HttpDelete("{BillID}")]
        public IActionResult Delete(int BillID)
        {
            var bill = _billRepository.BillDelete(BillID);
            if (!bill)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region insert

        [HttpPost]
        public IActionResult Add(BillModel bill)
        {
            if (bill == null)
            {
                return BadRequest();
            }
            bool isInserted = _billRepository.BillInsert(bill);
            if (isInserted)
            {
                return Ok(new { Message = "Bill Inserted" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion

        #region Update

        [HttpPut("{BillID}")]
        public IActionResult Update([FromBody] BillModel bill, int BillID)
        {
            if (bill == null || BillID != bill.BillID)
            {
                return BadRequest();
            }
            bool isInserted = _billRepository.BillUpdate(bill);

            if (isInserted)
            {
                return Ok(new { Message = "Bill Updated" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion

        #region OrderDropDown

        [HttpGet("Order")]
        public IActionResult OrderDropDown()
        {
            var order = _billRepository.OrderDropDown();
            if (!order.Any())
                return NotFound("No Order found.");

            return Ok(order);
        }

        #endregion

        #region CustomerDropDown

        [HttpGet("Customer")]
        public IActionResult CustomerDropDown()
        {
            var customer = _billRepository.CustomerDropDown();
            if (!customer.Any())
                return NotFound("No Customer found.");

            return Ok(customer);
        }
        #endregion
    }
}
