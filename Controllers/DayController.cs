using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TripPlanner.Models;

namespace TripPlanner.Controllers
{
    public class DaysController : Controller
    {
        private readonly TripPlannerContext _db;

        public DaysController(TripPlannerContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Day> model = _db.Days
                .OrderBy(days => days.Date)
                .ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Day day)
        {
            _db.Days.Add(day);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Day thisDay = _db.Days
                .FirstOrDefault(days => days.DayId == id);
            return View(thisDay);
        }

        public ActionResult Edit(int id)
        {
            Day thisDay = _db.Days
                .FirstOrDefault(days => days.DayId == id);
            return View(thisDay);
        }

        [HttpPost]
        public ActionResult Edit(Day day)
        {
            _db.Entry(day).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = day.DayId });
        }

        public ActionResult Delete(int id)
        {
            Day thisDay = _db.Days
                .FirstOrDefault(days => days.DayId == id);
            return View(thisDay);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Day thisDay = _db.Days
                .FirstOrDefault(days => days.DayId == id);
            _db.Days.Remove(thisDay);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}