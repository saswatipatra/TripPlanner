using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Models
{
     [Table("trips")]
    public class Trip
    {
        [Key]
        
        public int TripId {get; set;}
        public int UserProfileId {get; set;}
        public string TripName { get; set; }
        public string TripLocation {get; set;}
        public DateTime TripStartDate { get; set; }
        public DateTime TripEndDate { get; set; }

        public ICollection<Day> Days { get; set; }

        public Trip()
        {
            this.Days = new HashSet<Day>();
        }
    }
}