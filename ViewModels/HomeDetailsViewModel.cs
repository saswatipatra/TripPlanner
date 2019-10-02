using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using TripPlanner.Models;
namespace TripPlanner.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Userprofile Userprofile { get; set; }
        public Trip Trip {get;set;}
        
    }
}