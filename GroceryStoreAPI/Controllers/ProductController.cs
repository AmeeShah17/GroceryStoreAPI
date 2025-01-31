﻿using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _productRepository.SelectAll();
            return Ok(product);
        }

        [HttpGet("{ProductID}")]
        public IActionResult GetbyID(int ProductID)
        {
            var product = _productRepository.GetbyID(ProductID);
            return Ok(product);
        }

        [HttpDelete("{ProductID}")]
        public IActionResult Delete(int ProductID)
        {
            var product = _productRepository.ProductDelete(ProductID);
            if (!product)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult Add(ProductModel product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            bool isInserted = _productRepository.ProductInsert(product);
            if (isInserted)
            {
                return Ok(new { Message = "Product Inserted" });
            }
            return StatusCode(500, "An error occured during insert");
        }

        [HttpPut("{ProductID}")]
        public IActionResult Update([FromBody] ProductModel product, int ProductID)
        {
            if (product == null || ProductID != product.ProductID)
            {
                return BadRequest();
            }
            bool isInserted = _productRepository.ProductUpdate(product);

            if (isInserted)
            {
                return Ok(new { Message = "Product Updated" });
            }
            return StatusCode(500, "An error occured during insert");
        }
        [HttpGet("SubCategory")]
        public IActionResult SubCategoryDropDown()
        {
            var category = _productRepository.SubCategoryDropDown();
            if (!category.Any())
                return NotFound("No Sub Category found.");

            return Ok(category);
        }
    }
}
