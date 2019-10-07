using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TripPlanner.ViewModels;

namespace TripPlanner.Models
{
    [Table("userprofiles")]
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public string ApplicationUserId { get; set; }
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
         public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
         public static void CreateUserProfile(TripPlannerCreateViewModel userprofile)
        {
            var client = new RestClient("http://localhost:5000/");
            var request = new RestRequest("userprofiles/", Method.POST);
            request.AddJsonBody(userprofile);
            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
        }
    }
}