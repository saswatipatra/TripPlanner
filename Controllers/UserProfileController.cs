using Microsoft.AspNetCore.Mvc;
using TripPlanner.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TripPlanner.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TripPlanner.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly TripPlannerContext _db;
        private readonly IHostingEnvironment hostingEnvironment;
       private readonly UserManager<ApplicationUser> _userManager;

        public UserProfilesController(TripPlannerContext db, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet] 
        public ActionResult <UserProfile> Index(string id)
        {
            return _db.UserProfiles
                    .FirstOrDefault(x=> x.ApplicationUserId == id);
        }
        [HttpGet]
        public ActionResult Create(string id)
        {
            ViewBag.ApplicationUserId= id;
            return View();
        }


        [HttpPost]
        public IActionResult Create(TripPlannerCreateViewModel model)
        {   

            if (ModelState.IsValid)
            {
                Console.WriteLine("I'm inside user profile controller create function.....");
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Photo != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    Console.WriteLine(uploadsFolder);
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    Console.WriteLine(filePath);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                UserProfile newUserProfile = new UserProfile
                {
                    Name = model.Name,
                    // Store the file name in PhotoPath property of the UserProfile object
                    // which gets saved to the UserProfiles database table
                    Image = uniqueFileName,
                    Gender= model.Gender,
                    Age= model.Age,
                    Location = model.Location,
                    ApplicationUserId= model.ApplicationUserId
                }; 
                  Console.WriteLine("user profile create with user Id as {0} ", model.ApplicationUserId);
                 _db.UserProfiles.Add(newUserProfile);
                _db.SaveChanges();  
               
        }
            Console.WriteLine("Redirected to UserProfile index {0} ", model.ApplicationUserId);
            return RedirectToAction("Index","UserProfiles", new { id = model.ApplicationUserId });
        }
        public ActionResult Details(int id)
        {
            var thisUserProfile = _db.UserProfiles
                .Include(userProfile => userProfile.Trips)
                .FirstOrDefault(userProfile => userProfile.UserProfileId == id);
            return View(thisUserProfile);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var thisUserProfile = _db.UserProfiles.FirstOrDefault(UserProfiles => UserProfiles.UserProfileId == id);
            return View(thisUserProfile);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(UserProfile userProfile)
        {
            _db.Entry(userProfile).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var thisUserProfile = _db.UserProfiles.FirstOrDefault(UserProfiles => UserProfiles.UserProfileId == id);
            return View(thisUserProfile);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisUserProfile = _db.UserProfiles.FirstOrDefault(UserProfiles => UserProfiles.UserProfileId == id);

            _db.UserProfiles.Remove(thisUserProfile);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}