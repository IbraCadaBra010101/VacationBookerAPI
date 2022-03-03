using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VacationBookerAPI.Entities;

namespace VacationBookerAPI.Migrations;

public class AuthenticationContext : IdentityDbContext<User>
{
    private DbSet<User> User { get; set; }

    public AuthenticationContext(DbContextOptions options) : base(options)
    {
    }
}