using VacationBookerAPI.Primitives;

namespace VacationBookerAPI.DtoModels;

public class BookingDto
{
    public Guid Id { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDateDate { get; set; }
    public int NumberOfDays { get; set; }
    public BookingType BookingType { get; set; }

    public ApplicationStatus ApplicationStatus { get; set; }
    public Guid EmployeeId { get; set; }

    public string EmployeeFirstName { get; set; }

    public string EmployeeLastName { get; set; }
}