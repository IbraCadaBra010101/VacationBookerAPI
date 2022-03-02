using Microsoft.EntityFrameworkCore;
using VacationBookerAPI.Entities;
using VacationBookerAPI.Interfaces;
using VacationBookerAPI.Migrations;

namespace VacationBookerAPI.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly DataContext _context;

    public DepartmentRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<Department> CreateDepartment(Department department)
    {
        _context.Department.Add(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<IEnumerable<Department>> GetDepartments()
    {
        return await _context.Department.ToListAsync();
    }

    public async Task<Department?> GetDepartmentById(Guid? id)
    {
        return await _context.Department.FindAsync(id);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    
    public void UpdateDepartment(Department department)
    {
        var entry = _context.Entry(department);
        entry.CurrentValues.SetValues(department);
    }

    public void DeleteDepartment(Department? department)
    {
        _context.Entry(department).State = EntityState.Deleted;
    }
}