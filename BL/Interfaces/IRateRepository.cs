using E_Commerce_App.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.BL.Interfaces
{
    public interface IRateRepository
    {
        public void Add(Rate rate);
        public void Update(Rate rate);

        public bool IsFirstUserRate(string UserId, int ProductId);
        public int GetUserProductRate(string UserId,int ProductId);
        public double GetTotalProductRate(int ProductId);

    }
}
