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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace store_api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProductRepo _repository;
        public ProductController(IProductRepo repository, IMapper mapper , IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts()
        {

            
            var productsItems = _repository.GetAllProducts();
            productsItems = productsItems.Select(x => new Products()
            {
                Id= x.Id ,
                name = x.name,
                description = x.description,
                status = x.status,
                ImageName = x.ImageName,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
            });

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productsItems));
        }
        [HttpGet("{id}", Name = "GetProductsById")]
        public ActionResult<ProductReadDto> GetProductsById(int id)
        {
            var productItem = _repository.GetProductById(id);
            productItem.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, productItem.ImageName);
            if (productItem != null)
            {
                return Ok(_mapper.Map<ProductReadDto>(productItem));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<ProductCreateDto>> CreateProductAsync([FromForm] ProductCreateDto productCreateDto)
        {
            productCreateDto.ImageName = await SaveImage(productCreateDto.ImageFile);

            var productModel = _mapper.Map<Products>(productCreateDto);

            _repository.CreateProduct(productModel);
            _repository.SaveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(productModel);

            return Ok(productReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductAsync(int id, [FromForm] ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto.ImageFile != null)
            {
                DeleteImage(productUpdateDto.ImageName);
                productUpdateDto.ImageName = await SaveImage(productUpdateDto.ImageFile);
            }

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

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
