using Microsoft.AspNetCore.Mvc;
using TripPlanner.Models;
using System.Collections.Generic;

namespace TripPlanner.Controllers
{
    public class TripsController : Controller
    {
        [HttpGet("/trips")]
        public ActionResult Index()
        {
            List<Trip> allTrips = Trip.GetAll();
            return View(allTrips);
        }

        [HttpGet("/trips/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/trips")]
        public ActionResult Create(string userName, string tripName, int tripDate)
        {
            Trip myTrip = new Trip(userName, tripName, tripDate);
            return RedirectToAction("Index");
        }
    }
}