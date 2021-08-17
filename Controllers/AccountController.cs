using E_Commerce_App.DAL.Entites;
using E_Commerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager ;

        private readonly ILogger<AccountController> logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            SignInManager<ApplicationUser> signInManager 
        )
        {
            this.userManager = userManager;
            this.logger = logger;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed &&
                                    (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,false);
                if (result.Succeeded)
                {
                   return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email Or Password");  
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var EmailConfirmationlink = Url.Action("ConfirmEmail", "Account", new { Email = user.Email, Token = token  },Request.Scheme);
                    logger.Log(LogLevel.Warning, EmailConfirmationlink);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> ConfirmEmail(string Email,string Token)
        {
            var user = await userManager.FindByEmailAsync(Email);
            var result = await userManager.ConfirmEmailAsync(user, Token);
            if (result == null)
            {
                ViewBag.Error = "Email Cannot be confirmed";
            }
            return View();
        }
       
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if (ModelState.IsValid) {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                    logger.Log(LogLevel.Warning, ResetPasswordLink);
                    return RedirectToAction("ConfirmForgetPassword");
                }
                return RedirectToAction("ConfirmForgetPassword");
            }
            return View(model);
        }
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }
        public IActionResult ResetPassword(string Email, string Token)
        {
            if (Email == null || Token == null)
            {
                ModelState.AddModelError("", "Invalid Data");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync() ;

            return RedirectToAction("Index", "Home");
        }
    }
}
