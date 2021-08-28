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
        private readonly IProductRepository productRepsoitory;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IProductRepository productRepsoitory,UserManager<ApplicationUser> userManager)
        {
            this.productRepsoitory = productRepsoitory;
            this.userManager = userManager;
        }
         
        public IActionResult Index()
        {
            ViewBag.UserId = userManager.GetUserId(User);
            var result = productRepsoitory.
                GetProductsOrderByRate().Skip(0).Take(3).
                Select(p => new ProductVM() { Id = p.Id, Description = p.Description, PhotoPath = p.PhotoPath, Price = p.Price, Title = p.Title }); ;
            return View(result);
        }
        
        }   
    }
