using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using store_api.Data;
using store_api.Models;

namespace store_api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepo _repository;
        public ProductController(IProductRepo repository)
        {
            _repository = repository;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Products>> GetAllProducts()
        {
            var productsItems = _repository.GetAllProducts();

            return Ok(productsItems);
        }
        [HttpGet("{id}", Name = "GetProductsById")]
        public ActionResult<Products> GetProductsById(int id)
        {
            var productItem = _repository.GetProductById(id);
            if (productItem != null)
            {
                return Ok(productItem);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var ProductModelFromRepo = _repository.GetProductById(id);
            if (ProductModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteProduct(ProductModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Products product)
        {
            _repository.UpdateProduct(id , product);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpPost]
        public ActionResult<Products> CreateProduct(Products product)
        {
            _repository.CreateProduct(product);
            _repository.SaveChanges();

            return Ok(_repository.GetAllProducts());
        }

    }
}
