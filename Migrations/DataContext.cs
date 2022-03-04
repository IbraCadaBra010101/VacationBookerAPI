using Microsoft.EntityFrameworkCore;
using VacationBookerAPI.Entities;

namespace VacationBookerAPI.Migrations;  

public class DataContext : DbContext
{
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Booking> Booking { get; set; }
    public DbSet<Department> Department { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}