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

namespace TripPlanner.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly TripPlannerContext _db;
        private readonly IHostingEnvironment hostingEnvironment;

        public UserProfilesController(TripPlannerContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Index()
        {
            List<Userprofile> model = _db.UserProfiles.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(TripPlannerCreateViewModel model)
        {   
            if (ModelState.IsValid)
            {
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

                Userprofile newUserprofile = new Userprofile
                {
                    Name = model.Name,
                    // Store the file name in PhotoPath property of the UserProfile object
                    // which gets saved to the UserProfiles database table
                    Image = uniqueFileName
                };  
                 _db.UserProfiles.Add(newUserprofile);
                _db.SaveChanges();  
        }
        
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var thisUserProfile = _db.UserProfiles
                .Include(userProfile => userProfile.Trips)
                .ThenInclude(join => join.Trip)
                .FirstOrDefault(userProfile => userProfile.UserprofileId == id);
            return View(thisUserProfile);
        }
        public ActionResult Edit(int id)
        {
            var thisUserProfile = _db.UserProfiles.FirstOrDefault(UserProfiles => UserProfiles.UserprofileId == id);
            return View(thisUserProfile);
        }

        [HttpPost]
        public ActionResult Edit(Userprofile userProfile)
        {
            _db.Entry(userProfile).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var thisUserProfile = _db.UserProfiles.FirstOrDefault(UserProfiles => UserProfiles.UserprofileId == id);
            return View(thisUserProfile);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisUserProfile = _db.UserProfiles.FirstOrDefault(UserProfiles => UserProfiles.UserprofileId == id);

            _db.UserProfiles.Remove(thisUserProfile);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}