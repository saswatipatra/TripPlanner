using System.Collections.Generic;

namespace TripPlanner.Models
{
    public class Trip
    {
        public string _UserName { get; set; }
        public string _TripName { get; set; }
        public int _TripDate { get; set; }

        private static List<Trip> _instances = new List<Trip> {};
        public Trip(string userName, string tripName, int tripDate)
        {
            _UserName = userName;
            _TripName = tripName;
            _TripDate = tripDate;
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