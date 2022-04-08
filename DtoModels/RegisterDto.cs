using System.ComponentModel.DataAnnotations;

namespace VacationBookerAPI.Entities;

public class RegisterDto
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