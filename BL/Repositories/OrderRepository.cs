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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext db;

        public OrderRepository(AppDBContext db)
        {
            this.db = db;
        }
        public void Add(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public double Checkout(string UserId)
        {
            var total = 0.0;
            foreach (var order in GetUserOrders(UserId))
            {
                total += (order.Quantity * order.Product.Price);
            }
            ClearCart(UserId);
            return total;
        }

        public void ClearCart(string UserId)
        {
            var orders = db.Orders.Where(o => o.UserId == UserId);
            db.Orders.RemoveRange(orders);
            db.SaveChanges();
        }

        public int DecrementOrderQuantity(int orderId)
        {
            var oldOrder = db.Orders.Find(orderId); 
            if (oldOrder.Quantity > 1)
            {
                oldOrder.Quantity--;
                db.Entry(oldOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return oldOrder.Quantity--;
            }
            else
            {
                return oldOrder.Quantity;
            }
        }

        public void Delete(int orderId)
        {
            var order = db.Orders.Find(orderId);
            db.Orders.Remove(order);
            db.SaveChanges();
        }

        public IEnumerable<Order> GetUserOrders(string UserId)
        {
            var result = db.Users.
                Include(u => u.Orders).
                ThenInclude(o => o.Product).
                FirstOrDefault(u => u.Id == UserId);
            if (result != null)
            {
                return result.Orders;
            }
            return null;
        }

        public int IncrementOrderQuantity(int orderId)
        {
            var oldOrder = db.Orders.Find(orderId);
            oldOrder.Quantity++;
            db.Entry(oldOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return oldOrder.Quantity++;
        }

        public bool IsProductInUserCart(string UserId, int ProductId)
        {
            var result = db.Orders.FirstOrDefault(o => o.ProductId == ProductId && o.UserId == UserId);
            if (result == null) {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
