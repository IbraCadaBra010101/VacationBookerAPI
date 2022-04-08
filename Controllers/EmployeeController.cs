using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationBookerAPI.DtoModels;
using VacationBookerAPI.Entities;
using VacationBookerAPI.Interfaces;

namespace VacationBookerAPI.Controllers;

[ApiController]
[Route("api/Employee")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;


    public EmployeesController(
        IEmployeeRepository employeeRepository, 
        IDepartmentRepository departmentRepository,
        IMapper mapper
        )
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post(EmployeeDto employee)
    {
        if (!ModelState.IsValid) throw new ValidationException();
        if (employee == null) throw new NullReferenceException();

        var departmentDb = 
            await _departmentRepository.GetDepartmentById(employee.DepartmentId);
        var managerDb = 
            await _employeeRepository.GetEmployeeById(employee.ManagerId);

        var dbEmployee = new Employee
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            RoleTitle = employee.RoleTitle,
            AnnualAllocation = employee.AnnualAllocation,
            EmailAddress = employee.EmailAddress,
            Department = departmentDb,
            Manager = managerDb
        };
        await _employeeRepository.CreateEmployee(dbEmployee);
        await _employeeRepository.SaveAllAsync();
        var employeeDto = _mapper.Map<EmployeeDto>(dbEmployee);
        return Ok(employeeDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        try
        {
            var employeeDb = await _employeeRepository.GetEmployeeById(id);
            if (employeeDb == null) return NotFound();
            var employeeDto = _mapper.Map<EmployeeDto>(employeeDb);
            return Ok(employeeDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employeeListDb = await _employeeRepository.GetEmployees();
        var employeeListDto = _mapper.Map<List<EmployeeDto>>(employeeListDb);
        return Ok(employeeListDto);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeDto employee)
    {
        var employeeDb = await _employeeRepository.GetEmployeeById(id);
        var departmentDb = await _departmentRepository.GetDepartmentById(employee.DepartmentId);
        var managerDb = await _employeeRepository.GetEmployeeById(employee.ManagerId);
        if (employeeDb == null) return NotFound();

        employeeDb.FirstName = employee.FirstName;
        employeeDb.LastName = employee.LastName;
        employeeDb.RoleTitle = employee.RoleTitle;
        employeeDb.EmailAddress = employee.EmailAddress;
        employeeDb.Manager = managerDb;
        employeeDb.Department = departmentDb;

        if (employeeDb.Department != null && employeeDb.Department.Id != employee.DepartmentId)
        {
            var department = await _departmentRepository.GetDepartmentById(employee.DepartmentId);
            employeeDb.Department = department;
        }
        
        _employeeRepository.UpdateEmployee(employeeDb);
        await _employeeRepository.SaveAllAsync();

        var dtoEmployee = _mapper.Map<EmployeeDto>(employeeDb);
        return Ok(dtoEmployee);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        try
        {
            var employeeDb = await _employeeRepository.GetEmployeeById(id);
            if (employeeDb == null) return NotFound();
            _employeeRepository.DeleteEmployee(employeeDb);
            await _employeeRepository.SaveAllAsync();
            return Ok(employeeDb);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
}