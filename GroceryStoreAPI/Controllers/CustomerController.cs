using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var customer = _customerRepository.SelectAll();
            return Ok(customer);
        }
        #endregion
        #region GetbyID

        [HttpGet("{CustomerID}")]
        public IActionResult GetbyID(int CustomerID)
        {
            var customer = _customerRepository.GetbyID(CustomerID);
            return Ok(customer);
        }
        #endregion

        #region Delete

        [HttpDelete("{CustomerID}")]
        public IActionResult Delete(int CustomerID)
        {
            var customer = _customerRepository.CustomerDelete(CustomerID);
            if (!customer)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region insert

        [HttpPost]
        public IActionResult Add(CustomerModel customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            bool isInserted = _customerRepository.CustomerInsert(customer);
            if (isInserted)
            {
                return Ok(new { Message = "Customer Inserted" });
                
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion

        #region Update

        [HttpPut("{CustomerID}")]
        public IActionResult Update([FromBody] CustomerModel customer, int CustomerID)
        {
            if (customer == null || CustomerID != customer.CustomerID)
            {
                return BadRequest();
            }
            bool isInserted = _customerRepository.CustomerUpdate(customer);

            if (isInserted)
            {
                return Ok(new { Message = "Customer Inserted" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion
    }
}
