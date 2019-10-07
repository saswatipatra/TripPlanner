using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TripPlanner.Models;
namespace TripPlanner.ViewModels
{
public class TripPlannerCreateViewModel
{
    public int UserProfileId { get; set; }
    public string ApplicationUserId { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public IFormFile Photo { get; set; }
    public string Location { get; set; }
    public ICollection<Trip> Trips { get; set; } 
}
}

       