using E_Commerce_App.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.BL.Interfaces
{
    public interface IProductRepository
    {
        public void Add(Product product);
        public void Delete(int ProductId);
        public void Update(Product product);
        public IEnumerable<Product> Get();
        public Product Get(int ProductId);
        public IEnumerable<Product> GetProductsOrderByRate();
    }
}
