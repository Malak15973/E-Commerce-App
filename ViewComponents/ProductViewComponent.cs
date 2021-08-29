using E_Commerce_App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ProductVM product,string UserId)
        {
            ViewBag.UserId = UserId; 
            return View(product);
        }
    }
}
