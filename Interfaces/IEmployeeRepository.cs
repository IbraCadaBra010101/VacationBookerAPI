using VacationBookerAPI.DtoModels;
using VacationBookerAPI.Entities;

namespace VacationBookerAPI.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetEmployees();
    Task<Employee?> GetEmployeeById(Guid id); 
    Task<bool> SaveAllAsync();
    Task<Employee> CreateEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(Employee employee);
}