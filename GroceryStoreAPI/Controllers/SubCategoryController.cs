using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private SubCategoryRepository _subCategoryRepository;

        public SubCategoryController(SubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var subcategory = _subCategoryRepository.SelectAllSubCategory();
            return Ok(subcategory);
        }

        [HttpGet("{SubCategoryID}")]
        public IActionResult GetbyID(int SubCategoryID)
        {
            var subcategory = _subCategoryRepository.GetbyID(SubCategoryID);
            return Ok(subcategory);
        }

        [HttpDelete("{SubCategoryID}")]
        public IActionResult Delete(int SubCategoryID)
        {
            var subcategory = _subCategoryRepository.SubCategoryDelete(SubCategoryID);
            if (!subcategory)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult Add(SubCategoryModel subcategory)
        {
            if (subcategory == null)
            {
                return BadRequest();
            }
            bool isInserted = _subCategoryRepository.SubCategoryInsert(subcategory);
            if (isInserted)
            {
                return Ok(new { Message = "Sub Category Inserted" });
            }
            return StatusCode(500, "An error occured during insert");
        }

        [HttpPut("{SubCategoryID}")]
        public IActionResult Update([FromBody] SubCategoryModel subcategory, int SubCategoryID)
        {
            if (subcategory == null || SubCategoryID != subcategory.SubCategoryID)
            {
                return BadRequest();
            }
            bool isInserted = _subCategoryRepository.SubCategoryUpdate(subcategory);

            if (isInserted)
            {
                return Ok(new { Message = "Sub Category Updated" });
            }
            return StatusCode(500, "An error occured during insert");
        }

        [HttpGet("Category")]
        public IActionResult CategoryDropDown()
        {
            var category = _subCategoryRepository.CategoryDropDown();
            if (!category.Any())
                return NotFound("No Category found.");

            return Ok(category);
        }
    }
}
