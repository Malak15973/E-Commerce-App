using E_Commerce_App.BL.Interfaces;
using E_Commerce_App.DAL;
using E_Commerce_App.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.BL.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly AppDBContext db;

        public RateRepository(AppDBContext db)
        {
            this.db = db;
        }

        public void Add(Rate rate)
        {
            db.Rates.Add(rate);
            db.SaveChanges();
        }

        public double GetTotalProductRate(int ProductId)
        {
            double totalRate = 0.0;
            var result = db.Rates.Where(r => r.ProductId == ProductId);
            if (result.Count() == 0.0)
            {
                return totalRate;
            }
            foreach (var rate in result)
            {
                totalRate += rate.UserRate;
            }
            return totalRate / result.Count();
        }

        public int GetUserProductRate(string UserId, int ProductId)
        {
            var result = db.Rates.FirstOrDefault(r=>r.UserId==UserId&&r.ProductId==ProductId) ;
            if (result != null)
            {
                return result.UserRate;
            }
            return 0;
        }

        public bool IsFirstUserRate(string UserId, int ProductId)
        {
            var result = GetUserProductRate(UserId, ProductId);
            if(result != 0)
            {
                return false;
            }
            return true;
        }

        public void Update(Rate rate)
        {
            var oldRate = db.Rates.FirstOrDefault(r => r.ProductId == rate.ProductId && r.UserId == rate.UserId);
            oldRate.UserRate = rate.UserRate;
            db.Entry(oldRate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
