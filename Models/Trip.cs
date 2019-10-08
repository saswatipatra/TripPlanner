using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

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

        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Day> Days { get; set; }

        public Trip()
        {
            this.Days = new HashSet<Day>();
        }
    }
}