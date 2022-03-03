using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VacationBookerAPI.Entities;

public class User : IdentityUser
{
    [Column(TypeName = "nvarchar(150)")]
    public string FirstName { get; set; }
    
    [Column(TypeName = "nvarchar(150)")]
    public string LastName { get; set; }
}