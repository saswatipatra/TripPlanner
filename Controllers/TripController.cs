using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TripPlanner.Models;

namespace TripPlanner.Controllers
{
    public class TripsController : Controller
    {
        private readonly TripPlannerContext _db;

        public TripsController(TripPlannerContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Trip> model = _db.Trips
                .OrderBy(trips => trips.TripName)
                .ToList();
            return View(model);
        }

        public ActionResult Create(int id)
        {
            ViewBag.UserProfileId= id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Trip trip)
        {
            _db.Trips.Add(trip);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Trip thisTrip = _db.Trips
                .FirstOrDefault(trips => trips.TripId == id);
            ViewBag.Days = _db.Days
                .Where(appts => appts.TripId == id)
                .OrderBy(appts => appts.Date)
                .ToList();
            return View(thisTrip);
        }

        public ActionResult Edit(int id)
        {
            Trip thisTrip = _db.Trips
                .FirstOrDefault(trips => trips.TripId == id);
            return View(thisTrip);
        }

        [HttpPost]
        public ActionResult Edit(Trip trip)
        {
            _db.Entry(trip).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = trip.TripId });
        }

        public ActionResult Delete(int id)
        {
            Trip thisTrip = _db.Trips
                .FirstOrDefault(trips => trips.TripId == id);
            return View(thisTrip);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip thisTrip = _db.Trips
                .FirstOrDefault(trips => trips.TripId == id);
            _db.Trips.Remove(thisTrip);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}