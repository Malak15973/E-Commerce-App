using E_Commerce_App.BL.Interfaces;
using E_Commerce_App.DAL;
using E_Commerce_App.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.BL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext db;
        private readonly IRateRepository rateRepository;

        public ProductRepository(AppDBContext db, IRateRepository rateRepository)
        {
            this.db = db;
            this.rateRepository = rateRepository;
        }
        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }


        public IEnumerable<Product> Get()
        {
            return db.Products;
        }

        public Product Get(int ProductId)
        {
            return db.Products.Find(ProductId);
        }

        public IEnumerable<Product> GetProductsOrderByRate()
        {
            return Get().OrderByDescending(p => rateRepository.GetTotalProductRate(p.Id));
        }

       
    }
}
