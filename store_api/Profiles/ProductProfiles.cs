using AutoMapper;
using store_api.Dtos;
using store_api.Models;

namespace store_api.Profiles
{
    public class ProductProfiles: Profile
    {
        public ProductProfiles()
        {
            CreateMap<Products, ProductReadDto>();
            CreateMap<ProductCreateDto, Products>();
            CreateMap<ProductUpdateDto, Products>();
            CreateMap<Products, ProductUpdateDto>();
        }
    }
}
