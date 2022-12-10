using CosmosWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosWebAPI
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options) 
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
