using System.Collections.Generic;

namespace TripPlanner.Models
{
    public class Trip
    {
        public string _TripName { get; set; }
        public string _TripStartDate { get; set; }
        public string _TripEndDate { get; set; }

        private static List<Trip> _instances = new List<Trip> {};
        public Trip(string tripName, string startDate , string endDate)
        {
            _TripName = tripName;
            _TripStartDate = startDate;
            _TripEndDate = endDate;
            _instances.Add(this);
        }

      public static List<Trip> GetAll()
        {
            return _instances;
        }

        public static void ClearAll()
        {
            _instances.Clear();
        }
       
    }
}