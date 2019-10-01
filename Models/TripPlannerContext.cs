using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TripPlanner.Models;
namespace TripPlanner.Models

{
    public class TripPlannerContext : IdentityDbContext<ApplicationUser>
    {

        public virtual DbSet<Userprofile> UserProfiles { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Day> Days { get; set; }

        public TripPlannerContext(DbContextOptions options) : base(options) { }
    }
}