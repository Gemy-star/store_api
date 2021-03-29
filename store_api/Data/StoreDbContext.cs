using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using store_api.Models;

namespace store_api.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> opt) : base(opt)
        {
                
        }
        public DbSet<Products> products { get; set; }

    }
}
