using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Models
{
    [Table("userprofiles")]
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
       // public int ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        
        public ICollection<Trip> Trips { get; set; } 
        public UserProfile()
        {
            this.Trips = new HashSet<Trip>();
           
        }
    }
}