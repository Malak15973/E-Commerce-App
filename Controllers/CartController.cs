using E_Commerce_App.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using E_Commerce_App.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_App.Models;

namespace E_Commerce_App.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
        {
            this.orderRepository = orderRepository;
            this.userManager = userManager;
        }
        [Authorize]
        public IActionResult CartItems()
        {
            var cartItems = orderRepository.
                GetUserOrders(userManager.GetUserId(User)).
                Select(o=>new CartItemVM {OrderId=o.Id,Quantity=o.Quantity,PhotoPath = o.Product.PhotoPath,Price = o.Product.Price,Title=o.Product.Title });

            return View(cartItems);
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddToCart(int id)
        {
            Order order = new Order()
            {
                ProductId = id,
                UserId = userManager.GetUserId(User),
                Date = DateTime.Now,
                Quantity = 1
            };
            orderRepository.Add(order);
            return RedirectToAction("CartItems");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int id)
        {
            orderRepository.Delete(id);
            return RedirectToAction("CartItems");
        }
        [Authorize]
        public IActionResult Checkout()
        {
            var result = orderRepository.Checkout(userManager.GetUserId(User));
            return View(result);
        }

        [HttpPost]
        public JsonResult IncrementOrderQuantity(int orderId)
        {
            var newQuantity = orderRepository.IncrementOrderQuantity(orderId);
            return Json(newQuantity );
        }
        [HttpPost]
        public JsonResult DecrementOrderQuantity(int orderId)
        {
            var newQuantity = orderRepository.DecrementOrderQuantity(orderId);
            return Json(newQuantity);
        }
    }
}
