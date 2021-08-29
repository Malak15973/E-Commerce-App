using E_Commerce_App.BL.Interfaces;
using E_Commerce_App.DAL.Entites;
using E_Commerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepsoitory;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IOrderRepository orderRepository,IProductRepository productRepsoitory,UserManager<ApplicationUser> userManager)
        {
            this.orderRepository = orderRepository;
            this.productRepsoitory = productRepsoitory;
            this.userManager = userManager;
        }
         
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            ViewBag.UserId = userId;
            var result = productRepsoitory.
                GetProductsOrderByRate().Skip(0).Take(3).
                Select(p => new ProductVM() { Id = p.Id, Description = p.Description, PhotoPath = p.PhotoPath, Price = p.Price, Title = p.Title ,IsInUserCart=orderRepository.IsProductInUserCart(userId,p.Id)}); ;
            return View(result);
        }
        
        }   
    }
