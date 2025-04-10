﻿using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #region Getall

        [HttpGet]
        public IActionResult GetAll()
        {
            var category = _categoryRepository.SelectAll();
            return Ok(category);
        }
        #endregion

        #region GetbyID

        [HttpGet("{CategoryID}")]
        public IActionResult GetbyID(int CategoryID)
        {
            var category = _categoryRepository.GetbyID(CategoryID);
            return Ok(category);
        }
        #endregion

        #region Delete

        [HttpDelete("{CategoryID}")]
        public IActionResult Delete(int CategoryID)
        {
            var category=_categoryRepository.CategoryDelete(CategoryID);
            if(!category)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region insert
        [HttpPost]
        public IActionResult Add( CategoryModel category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            bool isInserted = _categoryRepository.CategoryInsert(category);
            if (isInserted)
            {
                return Ok(new { Message = "Category Inserted" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion

        #region Update

        [HttpPut("{CategoryID}")]
        public IActionResult Update([FromBody] CategoryModel category, int CategoryID)
        {
            if (category == null || CategoryID != category.CateoryID)
            {
                return BadRequest();
            }
            bool isInserted = _categoryRepository.CategoryUpdate(category);

            if (isInserted)
            {
                return Ok(new { Message = "Category Updated" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        #endregion
    }
}
