using E_Commerce_App.BL.Interfaces;
using E_Commerce_App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.ViewComponents
{
    public class UserRateViewComponent : ViewComponent
    {
        private readonly IRateRepository rateRepository;

        public UserRateViewComponent(IRateRepository rateRepository)
        {
            this.rateRepository = rateRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string UserId,int ProductId)
        {
            var result = rateRepository.GetUserProductRate(UserId, ProductId);
            RateVM model = new RateVM
            {
                ProductId = ProductId,
                UserId = UserId,
                UserRate = result
            };
            return View(model);
        }
    }
}
