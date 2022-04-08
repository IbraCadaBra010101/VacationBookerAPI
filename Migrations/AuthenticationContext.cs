using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VacationBookerAPI.Migrations;

public class AuthenticationContext : IdentityDbContext
{
    

    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
        
    }
}