using Microsoft.EntityFrameworkCore;

namespace MyProjectService.Infrastructure.Postgres
{



public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }







}

}