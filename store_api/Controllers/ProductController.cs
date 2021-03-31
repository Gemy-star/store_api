using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using store_api.Data;
using store_api.Models;
using store_api.Dtos;
using AutoMapper;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace store_api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _repository;
        public ProductController(IProductRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            var productsItems = _repository.GetAllProducts();

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productsItems));
        }
        [HttpGet("{id}", Name = "GetProductsById")]
        public ActionResult<ProductReadDto> GetProductsById(int id)
        {
            var productItem = _repository.GetProductById(id);
            if (productItem != null)
            {
                return Ok(_mapper.Map<ProductReadDto>(productItem));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<ProductCreateDto> CreateProduct(ProductCreateDto productCreateDto)
        {
           
            var productModel = _mapper.Map<Products>(productCreateDto);
            _repository.CreateProduct(productModel);
            _repository.SaveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(productModel);

            return Ok(productReadDto);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id,ProductUpdateDto productUpdateDto)
        {

            var productModelFromRepo = _repository.GetProductById(id);
            if (productModelFromRepo == null)
            {
                return NotFound();
            }
            var productModel = _mapper.Map<Products>(productUpdateDto);
            _repository.UpdateProduct(id, productModel);

            _repository.SaveChanges();

            return Ok();
        }
        [EnableCors]
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var productModelFromRepo = _repository.GetProductById(id);
            if (productModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteProduct(productModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

    }
}
