using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Models;
using System.Threading.Tasks;
using TripPlanner.ViewModels;
using TripPlanner.Controllers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;


namespace TripPlanner.Controllers
{
    public class AccountController : Controller
    {
        private readonly TripPlannerContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
         private readonly IHostingEnvironment hostingEnvironment;

       
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TripPlannerContext db, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            this.hostingEnvironment = hostingEnvironment;
           

        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
           
            if (model.Password == model.ConfirmPassword)
            {
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.ApplicationUserId= user.Id;
                    Console.WriteLine("registration successful with user Id as ", ViewBag.ApplicationUserId);
                                        
                   return RedirectToAction("Create", "UserProfiles", new { id = ViewBag.ApplicationUserId});
                   
                }
                else
                {
                    ViewBag.ErrorMessage = "Registration Unsuccessful. A password must include at least six characters, a capital letter, a number, and a special character. If your password meets these requirements, try registering with a different username.";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Your passwords didn't match! Try again!";
                return View();
            }
        }
        

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await
            _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                return RedirectToAction("Index", "UserProfiles", new { id = ViewBag.UserProfileId});
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}