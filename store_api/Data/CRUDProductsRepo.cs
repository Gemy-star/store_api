using store_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_api.Data
{
    public class CRUDProductsRepo : IProductRepo
    {
        private readonly StoreDbContext _context;

        public CRUDProductsRepo(StoreDbContext context)
        {
            _context = context;
        }
        public void CreateProduct(Products pro)
        {

            if (pro == null)
            {
                throw new ArgumentNullException(nameof(pro));
            }

            _context.products.Add(pro);
        }

        public void DeleteProduct(Products pro)
        {
            if (pro == null)
            {
                throw new ArgumentNullException(nameof(pro));
            }
            _context.products.Remove(pro);
        }

        public IEnumerable<Products> GetAllProducts()
        {

            return _context.products.ToList();

        }


        public Products GetProductById(int id)
        {
            return _context.products.FirstOrDefault(p => p.Id == id);

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateProduct(int id ,Products pro)
        {
            if (pro == null)
            {
                throw new ArgumentNullException(nameof(pro));
            }

            var productFromDb =  _context.products.Find(id);
            productFromDb.name = pro.name;
            productFromDb.description = pro.description;
        }
    }
}
