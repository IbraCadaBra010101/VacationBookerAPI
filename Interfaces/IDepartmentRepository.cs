using VacationBookerAPI.Entities;

namespace VacationBookerAPI.Interfaces;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetDepartments();
    Task<Department?> GetDepartmentById(Guid? id);
    Task<bool> SaveAllAsync(); 
    Task<Department> CreateDepartment(Department department);  
    void UpdateDepartment(Department department);
    void DeleteDepartment(Department? department);
}