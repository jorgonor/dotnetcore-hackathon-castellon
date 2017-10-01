using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;


namespace examples_dotnet_core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Models.ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Models.Vehicle> Vehicles { get; set; }
    }
}