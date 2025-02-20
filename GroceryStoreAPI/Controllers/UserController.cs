using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public UserController(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        #region GetAll

        [HttpGet]
        public IActionResult GetAll()
        {
            var user = _userRepository.SelectAll();
            return Ok(user);
        }
        #endregion

        #region GetbyID

        [HttpGet("{UserID}")]
        public IActionResult GetbyID(int UserID)
        {
            var user = _userRepository.GetbyID(UserID);
            return Ok(user);
        }
        #endregion

        #region Delete

        [HttpDelete("{UserID}")]
        public IActionResult Delete(int UserID)
        {
            var user = _userRepository.UserDelete(UserID);
            if (user)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Insert

        [HttpPost]
        public IActionResult Add(UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            bool isInserted = _userRepository.UserInsert(user);
            if (isInserted)
            {
                return Ok(new { Message = "User Inserted" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion
        #region Register
        [HttpPost]
        public IActionResult Register(UserRegisterModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            bool isInserted = _userRepository.UserRegister(user);
            if (isInserted)
            {
                return Ok(new { Message = "User Registered" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion

        #region Update

        [HttpPut("{UserID}")]
        public IActionResult Update([FromBody] UserModel user, int UserID)
        {
            if (user == null || UserID != user.UserID)
            {
                return BadRequest();
            }
            bool isInserted = _userRepository.UserUpdate(user);

            if (isInserted)
            {
                return Ok(new { Message = "User Updated" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion

        #region User Login
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginModel user)
        {
            var userData = _userRepository.Login(user);
            if (userData != null)
            {
                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub,userData.UserID.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, userData.UserID.ToString()),
            new Claim(ClaimTypes.Name,userData.UserName.ToString()),
            new Claim(ClaimTypes.Email, userData.Email)
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
                return Ok(new { Token = tokenValue, User = userData, Message = "User Login Successfully" });
            }

            return BadRequest(new { Message = "Please enter valid Email and password" });
        }

        #endregion

        [HttpGet]
        [Authorize]
        public IActionResult GetProfile()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            var userIDClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                return Unauthorized();
            }
            int userid = int.Parse(userIDClaim.Value);

            var user = _userRepository.GetUserProfile(userid);

           

            return Ok(user);
        }

    }
}
