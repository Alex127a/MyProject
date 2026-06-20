using Microsoft.EntityFrameworkCore;
using MyProjectService.Domain;

namespace MyProjectService.Infrastructure.Postgres
{



    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            


        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<DepartmentLocation> DepartmentLocations { get; set; }
        public DbSet<DepartmentPosition> DepartmentPositions { get; set; }
    
        
    
    
    
    }
  

}