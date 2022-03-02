using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationBookerAPI.DtoModels;
using VacationBookerAPI.Entities;
using VacationBookerAPI.Interfaces;


namespace VacationBookerAPI.Controllers;

[ApiController]
[Route("api/Booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public BookingController(IDepartmentRepository departmentRepository, IMapper mapper,
        IBookingRepository bookingRepository, IEmployeeRepository employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    [HttpPost()]
    public async Task<IActionResult> Post(BookingDto booking)
    {
        if (!ModelState.IsValid) throw new ValidationException();
        if (booking == null) throw new NullReferenceException();
        var employeeDb = await _employeeRepository.GetEmployeeById(booking.EmployeeId);

        var bookingDb = new Booking
        {
            Id = booking.Id,
            FromDate = booking.FromDate,
            ToDateDate = booking.ToDateDate,
            NumberOfDays = booking.NumberOfDays,
            BookingType = booking.BookingType,
            ApplicationStatus = booking.ApplicationStatus,
            Employee = employeeDb,
            EmployeeId = booking.EmployeeId,
            EmployeeFirstName = employeeDb.FirstName,
            EmployeeLastName = employeeDb.LastName
        };

        await _bookingRepository.CreateBooking(bookingDb);
        await _bookingRepository.SaveAllAsync();

        var bookingDto = _mapper.Map<BookingDto>(bookingDb);

        return Ok(bookingDto);
    }

    [HttpGet("departmentBookings/{id:guid}")]
    public async Task<IActionResult> GetBookingsByDepartment(Guid id)
    {
        try
        {
            var departmentDb = await _departmentRepository.GetDepartmentById(id);
            if (departmentDb == null) NotFound();

            var bookingsListDb = await _bookingRepository.GetBookingsByDepartment(departmentDb.Id);
            var bookingListDto = _mapper.Map<List<BookingDto>>(bookingsListDb);

            return Ok(bookingListDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpGet()]
    public async Task<IActionResult> GetBookings()
    {
        try
        {
            var bookingsListDb = await _bookingRepository.GetBookings();
            if (bookingsListDb?.Any() == null) NotFound();
            var bookingListDto = _mapper.Map<List<BookingDto>>(bookingsListDb);
            return Ok(bookingListDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpGet("employeeBookings/{id:guid}")]
    public async Task<IActionResult> GetBookingsByEmployee(Guid id)
    {
        try
        {
            var employeeBookings = await _bookingRepository.GetBookingsByEmployee(id);
            var employeeBookingsDto = _mapper.Map<List<BookingDto>>(employeeBookings);
            return Ok(employeeBookingsDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateBooking(Guid id, BookingDto booking)
    {
        var bookingDb = await _bookingRepository.GetBookingById(id);
        if (bookingDb == null) return NotFound();

        bookingDb.BookingType = booking.BookingType;
        bookingDb.ApplicationStatus = booking.ApplicationStatus;
        bookingDb.FromDate = booking.FromDate;
        bookingDb.ToDateDate = booking.ToDateDate;
        bookingDb.NumberOfDays = booking.NumberOfDays;

        _bookingRepository.UpdateBooking(bookingDb);
        await _employeeRepository.SaveAllAsync();

        var dtoBooking = _mapper.Map<BookingDto>(bookingDb);
        return Ok(dtoBooking);
    }
}