using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Models
{
    [Table("days")]
    public class Day
    {
        [Key]
        public int DayId { get; set; }
        public int TripId {get; set;}
        public string Note { get; set; }
       public DateTime Date { get; set; }
       
    }
}