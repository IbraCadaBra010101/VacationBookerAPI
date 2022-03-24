using Microsoft.EntityFrameworkCore;
using VacationBookerAPI.Entities;
using VacationBookerAPI.Interfaces;
using VacationBookerAPI.Migrations;

namespace VacationBookerAPI.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly DataContext _context;

    public BookingRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> GetBookings()
    {
        return await _context.Booking.ToListAsync();
    }

    public async Task<Booking?> GetBookingById(Guid id)
    {
        return await _context.Booking
            .Include(e => e.Id)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Booking> CreateBooking(Booking booking)
    {
        _context.Booking.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public void UpdateBooking(Booking booking)
    {
        var entry = _context.Entry(booking);
        entry.CurrentValues.SetValues(booking);
    }

    public async Task<IEnumerable<Booking>> GetBookingsByDepartment(Guid departmentId)
    {
        var bookings = await _context.Booking.Where(b =>
                b.Employee != null && b.Employee.Department != null && b.Employee.Department.Id == departmentId)
            .ToArrayAsync();
        return bookings;
    }

    public async Task<IEnumerable<Booking>> GetBookingsByEmployee(Guid employeeId)
    {
        var employeeBookings = await _context.Booking.Where(b => b.Employee != null && b.Employee.Id == employeeId)
            .ToArrayAsync();
        return employeeBookings;
    }

    public void DeleteBooking(Booking booking)
    {
        _context.Entry(booking).State = EntityState.Deleted;
    }
}