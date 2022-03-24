namespace VacationBookerAPI.Entities;

using System.ComponentModel.DataAnnotations;

public class Employee
{
    public Guid Id { get; set; }
    [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string LastName { get; set; }
    [MaxLength(20)] public string RoleTitle { get; set; }
    public string EmailAddress { get; set; } 
    public int AnnualAllocation { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public virtual ICollection<Booking> Bookings { get; set; }
    public virtual Department? Department { get; set; }
    public Employee? Manager { get; set; }
} 