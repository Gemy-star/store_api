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
        public ActionResult<ProductCreateDto> CreateProduct([FromForm]ProductCreateDto productCreateDto)
        {
            Products product = new Products { name = productCreateDto.name , description= productCreateDto.description , status=productCreateDto.status };
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(productCreateDto.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)productCreateDto.Image.Length);
            }
            product.ImageData = imageData;

            _repository.CreateProduct(product);
            var commandModel = _mapper.Map<Products>(product);
            _repository.CreateProduct(commandModel);
            _repository.SaveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(commandModel);

            return Ok(productReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, [FromForm] ProductUpdateDto productUpdateDto)
        {
            Products product = new Products { name = productUpdateDto.name, description = productUpdateDto.description, status = productUpdateDto.status };
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(productUpdateDto.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)productUpdateDto.Image.Length);
            }
            product.ImageData = imageData;
            var productModelFromRepo = _repository.GetProductById(id);
            if (productModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.UpdateProduct(id,product);

            _repository.SaveChanges();

            return NoContent();
        }

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
