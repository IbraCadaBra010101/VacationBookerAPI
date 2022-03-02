using Microsoft.EntityFrameworkCore;
using VacationBookerAPI.Entities;
using VacationBookerAPI.Interfaces;
using VacationBookerAPI.Migrations;

namespace VacationBookerAPI.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DataContext _context;

    public EmployeeRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        _context.Employee.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<List<Employee>> GetEmployees()
    {
        return await _context.Employee
            .Include(dep => dep.Department)
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeById(Guid id)
    {
        return await _context.Employee
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void UpdateEmployee(Employee employee)
    {
        var entry = _context.Entry(employee);
        entry.CurrentValues.SetValues(employee);
    }

    public void DeleteEmployee(Employee employee)
    {
        _context.Entry(employee).State = EntityState.Deleted;
    }
}