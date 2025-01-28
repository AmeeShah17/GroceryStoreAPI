using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var user = _userRepository.SelectAll();
            return Ok(user);
        }

        [HttpGet("{UserID}")]
        public IActionResult GetbyID(int UserID)
        {
            var user = _userRepository.GetbyID(UserID);
            return Ok(user);
        }

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
    }
}
