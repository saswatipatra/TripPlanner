using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Models;

namespace TripPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/")]
        public async Task<ActionResult> Index()
        {
                Console.WriteLine("HomeController userContext {0}", User);
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    Console.WriteLine("login for first time");
                    return View();
                    
                }
                Console.WriteLine("home index page test {0}", user.Id);
                ViewBag.ApplicationUserId = user.Id.ToString();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LogOff()
        {
            return RedirectToAction("LogOff", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
