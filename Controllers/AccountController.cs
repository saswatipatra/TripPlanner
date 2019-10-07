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
                    Console.WriteLine("registration successful");
                    Console.WriteLine("Model.ID: ", model.Id);
                    var newUserProfile = new TripPlannerCreateViewModel { Name = model.UserName, Gender = "", Age = 0, Location = "", ApplicationUserId = user.Id };
                    
                   
                    return await Create(newUserProfile);
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
        [HttpPost]
        public async Task<ActionResult> Create(TripPlannerCreateViewModel model)
        {   
            Console.WriteLine(".......I'm inside Create Function!.......");
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                
                Console.WriteLine("........Photo : ", model.Photo);
                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Photo != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine
                    (hostingEnvironment.WebRootPath, "images");
                    Console.WriteLine(uploadsFolder);
                    
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    Console.WriteLine("........... File Path", filePath);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                UserProfile newUserProfile = new UserProfile
                {
                    Name = model.Name,
                    // Store the file name in PhotoPath property of the UserProfile object
                    // which gets saved to the UserProfiles database table
                    Image = uniqueFileName
                };  
                 _db.UserProfiles.Add(newUserProfile);
                _db.SaveChanges();  
                ViewBag.UserProfileId= newUserProfile.UserProfileId;
                

        }
        
            return RedirectToAction("Login");
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