using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudProducts.Model;

namespace CrudProducts.Data
{
    public class CrudProductsContext : DbContext
    {
        public CrudProductsContext (DbContextOptions<CrudProductsContext> options)
            : base(options)
        {
        }
        public Product GetProductById(int productId)
        {
            return Product.FirstOrDefault(p => p.Id == productId);
        }

        public DbSet<CrudProducts.Model.Product> Product { get; set; }

        public DbSet<CrudProducts.Model.Category> Category { get; set; }
    }
}
