using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;


namespace GroceryStoreAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository _customerRepository;
         private readonly IConfiguration _configuration;


        public CustomerController(CustomerRepository customerRepository, IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
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
        #region CustomerRegister
        [HttpPost]
        public IActionResult Register(CustomerRegisterModel customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            bool isInserted = _customerRepository.CustomerRegister(customer);
            if (isInserted)
            {
                return Ok(new { Message = "Customer Register" });

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

        #region Customer Login
        [HttpPost]
        public IActionResult Login([FromBody] CustomerLoginModel customer)
        {
            var customerData = _customerRepository.CustomerLogin(customer);
            if (customerData != null)
            {
                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub,customerData.CustomerID.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, customerData.CustomerID.ToString()),
            new Claim(ClaimTypes.Name, customerData.CustomerName),
            new Claim(ClaimTypes.Email, customerData.Email)
        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: signIn
                );

                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tokenValue, Customer = customerData, Message = "Customer Login Successfully" });
            }

            return BadRequest(new { Message = "Please enter valid Email and password" });
        }

        #endregion

        [HttpGet]
        [Authorize]
        public IActionResult GetProfile()
        {
            var identity = User.Identity as ClaimsIdentity;
            if(identity == null)
            {
                return Unauthorized();
            }
            var customerIDClaim=identity.FindFirst(ClaimTypes.NameIdentifier);
            if (customerIDClaim == null)
            {
                return Unauthorized();
            }
            int customerId=int.Parse(customerIDClaim.Value);
            
            var customer = _customerRepository.GetCustomerProfile(customerId);

            //if (customer == null)
            //{
            //    return NotFound(new { Message = "Customer not found" });
            //}

            return Ok(customer);
        }



    }
}
