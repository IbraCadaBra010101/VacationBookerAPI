using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VacationBookerAPI.Entities;

public class Register : IdentityUser
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Please enter at least two characters ")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Please enter at least two characters ")]
    public string LastName { get; set; }
}