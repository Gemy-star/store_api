using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using store_api.Models;
namespace store_api.Data
{
    public interface IProductRepo
    {
        bool SaveChanges();

        IEnumerable<Products> GetAllProducts();
        Products GetProductById(int id);
        void CreateProduct(Products pro);
        void UpdateProduct(int id,Products pro);
        void DeleteProduct(Products pro);
    }
}
