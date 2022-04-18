using DeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Vehiculo> Vehiculos { get; set; }

    }
}
