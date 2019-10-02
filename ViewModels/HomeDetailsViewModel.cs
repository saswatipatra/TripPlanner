using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using TripPlanner.Models;
namespace TripPlanner.ViewModels
{
    public class HomeDetailsViewModel
    {
        public UserProfile UserProfile { get; set; }
        public Trip Trip {get;set;}
        
    }
}