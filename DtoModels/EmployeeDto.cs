namespace VacationBookerAPI.DtoModels;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string RoleTitle { get; set; }
    public string EmailAddress { get; set; }
    public int AnnualAllocation { get; set; }

    public DateTime StartDate { get; set; }

    public string DepartmentName { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid ManagerId { get; set; }

    public string ManagerFirstName { get; set; }

    public string ManagerLastName { get; set; }
}