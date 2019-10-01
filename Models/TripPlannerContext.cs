using Microsoft.EntityFrameworkCore;

namespace TripPlanner.Models
{
    public class TripPlannerContext : DbContext
    {
        public virtual DbSet<Userprofile> UserProfiles { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Day> Days { get; set; }

        public TripPlannerContext(DbContextOptions options) : base(options) { }
    }
}