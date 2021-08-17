using E_Commerce_App.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.BL.Interfaces
{
    public interface IOrderRepository
    {
        public void Add(Order order);
        public void Delete(int orderId);
        public IEnumerable<Order> GetUserOrders(string UserId);
        public int IncrementOrderQuantity(int orderId);
        public int DecrementOrderQuantity(int orderId);
        public bool IsProductInUserCart(string UserId,int ProductId);
        public double Checkout(string UserId);
        public void ClearCart(string UserId);

    }
}
