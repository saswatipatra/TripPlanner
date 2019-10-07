using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TripPlanner.Models
{
    public class TripPlannerContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Day> Days { get; set; }

        public TripPlannerContext(DbContextOptions options) : base(options) { }
    }
}