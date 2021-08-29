using E_Commerce_App.BL.Interfaces;
using E_Commerce_App.DAL.Entites;
using E_Commerce_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IRateRepository rateRepository;
        private readonly IOrderRepository orderRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductController(IProductRepository productRepository, IRateRepository rateRepository,IOrderRepository orderRepository ,UserManager<ApplicationUser> userManager)
        {
            this.productRepository = productRepository;
            this.rateRepository = rateRepository;
            this.orderRepository = orderRepository;
            this.userManager = userManager;
        }
        public IActionResult Products(int skip)
        {
            var userId = userManager.GetUserId(User);
            var products = productRepository.Get();
            ViewBag.ProductsCount = products.Count();
            ViewBag.UserId = userId;
            var result = products.Skip(skip).Take(5).
                Select(p=> new ProductVM() {Id = p.Id,Description= p.Description,PhotoPath=p.PhotoPath,Price = p.Price ,Title=p.Title,IsInUserCart=orderRepository.IsProductInUserCart(userId,p.Id) });
            return View(result);
        }
        [Authorize]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(CreateProductVM model)
        {
            if (ModelState.IsValid)
            {
                string PhotoPath = Directory.GetCurrentDirectory() + "/wwwroot/Files/Photos";
                string PhotoName = Guid.NewGuid() + "_" + model.Photo.FileName;
                string FinalPath = Path.Combine(PhotoPath, PhotoName);
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    model.Photo.CopyTo(Stream);
                }
                Product product = new Product
                {
                    Description = model.Description,
                    PhotoPath = PhotoName,
                    Price = model.Price,
                    Title = model.Title
                };
                productRepository.Add(product);
                return RedirectToAction("Products");
            }
            return View(model);
        }

        //-----------------------------------Ajax----------------------------
        [HttpPost]
        public JsonResult RateProduct(RateVM model)
        {
            if (rateRepository.IsFirstUserRate(model.UserId, model.ProductId))
            {
                rateRepository.Add(new Rate { UserId = model.UserId, ProductId = model.ProductId, UserRate = model.UserRate });
            }
            else
            {
                rateRepository.Update(new Rate { UserId = model.UserId, ProductId = model.ProductId, UserRate = model.UserRate });
            }
            var totalPostRate = rateRepository.GetTotalProductRate(model.ProductId);
            return Json(totalPostRate);
        }
    }
}
