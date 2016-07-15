using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HomeTask1.DAL.Models.Mapping;

namespace HomeTask1.DAL.Models
{
    public partial class WeatherDBContext : DbContext
    {
        static WeatherDBContext()
        {
            Database.SetInitializer<WeatherDBContext>(null);
        }

        public WeatherDBContext()
            : base("Name=WeatherDBContext")
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Query> Queries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new QueryMap());
        }
    }
}
