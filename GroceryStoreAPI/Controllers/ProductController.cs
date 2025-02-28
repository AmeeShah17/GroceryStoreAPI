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
    public class ProductController : ControllerBase
    {
        private ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #region GetAll

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _productRepository.SelectAll();
            return Ok(product);
        }
        #endregion

        #region GetbyID

        [HttpGet("{ProductID}")]
        public IActionResult GetbyID(int ProductID)
        {
            var product = _productRepository.GetbyID(ProductID);
            return Ok(product);
        }
        #endregion

        #region Delete

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
        #endregion

        #region Insert

        [HttpPost]
        public IActionResult Add(ProductModel product)
        {
            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ImageFile.CopyTo(fileStream);
                }

                product.ProductImage = "/ProductImages/" + uniqueFileName; // Return URL instead of Base64
            }

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
        #endregion

        #region Update

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
        #endregion

        #region SubCategoryDropDown
        [HttpGet("SubCategory")]
        public IActionResult SubCategoryDropDown()
        {
            var category = _productRepository.SubCategoryDropDown();
            if (!category.Any())
                return NotFound("No Sub Category found.");

            return Ok(category);
        }
        #endregion

        #region GetProductBySubCatgeory

        [HttpGet("{SubCategoryID}")]
        public IActionResult GetProductsBySubCategory(int SubCategoryID)
        {
            if (SubCategoryID <= 0)
                return BadRequest("Invalid SubCategoryID.");

            var products = _productRepository.GetProductBySubCategory(SubCategoryID);

            if (products == null || !products.Any())
                return NotFound("No products found for the given SubCategoryID.");

            return Ok(products);
        }

        #endregion
    }
}
