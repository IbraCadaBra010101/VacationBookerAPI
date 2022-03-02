using VacationBookerAPI.Entities;

namespace VacationBookerAPI.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetBookings();
    Task<Booking?> GetBookingById(Guid id);
    Task<bool> SaveAllAsync();
    Task<Booking> CreateBooking(Booking booking);
    Task<IEnumerable<Booking>> GetBookingsByDepartment(Guid departmentId);
    Task<IEnumerable<Booking>> GetBookingsByEmployee(Guid employeeId);
    void UpdateBooking(Booking booking);
    void DeleteBooking(Booking booking);
}