using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Riddle.Services.Email;
using Riddle.ViewModels;

namespace Riddle.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IEmailService emailService)
            
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]   // this will display the login page
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]   // this will allow us to capture Login form 
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _signInManager
                .PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            //redirect to only admin to panel
            if(!result.Succeeded)
            {
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Panel");
        }

        [HttpGet]   // this will display the login page
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]   // this will allow us to capture Login form 
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var user = new IdentityUser
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email
            };
             
            var result = await _userManager.CreateAsync(user, "password");

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _emailService.SendEmail(user.Email, "WelCome", "Thank You for Registering!!!");
                return RedirectToAction("Index","Home");
            }

            return View(registerViewModel);
        }

        [HttpGet]  
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}