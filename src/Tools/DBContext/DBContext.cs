using Microsoft.EntityFrameworkCore;
using telemetry_api.Import.Model;

namespace ChargeMe.API.DataBase
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        
        public DbSet<MoistureSQL> Moisture { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoistureSQL>().ToTable("Telemetry");
        }
    }
}
