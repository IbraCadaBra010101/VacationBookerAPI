using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationBookerAPI.DtoModels;
using VacationBookerAPI.Entities;
using VacationBookerAPI.Interfaces;

namespace VacationBookerAPI.Controllers;

[ApiController]
[Route("api/Department")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    [HttpPost()]
    public async Task<IActionResult> Post(DepartmentDto department)
    {
        if (!ModelState.IsValid) throw new ValidationException();
        if (department == null) throw new NullReferenceException();
        
        var dbDepartment = _mapper.Map<Department>(department);

        await _departmentRepository.CreateDepartment(dbDepartment);
        await _departmentRepository.SaveAllAsync();
        var departmentDto = _mapper.Map<DepartmentDto>(dbDepartment);

        return Ok(departmentDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDepartmentById(Guid id)
    {
        try
        {
            var departmentDb = await _departmentRepository.GetDepartmentById(id);
            var departmentDto =  _mapper.Map<DepartmentDto>(departmentDb);
            return Ok(departmentDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        var departmentListDb = await _departmentRepository.GetDepartments();
        var departmentListDto = _mapper.Map<List<DepartmentDto>>(departmentListDb);
        return Ok(departmentListDto);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateDepartment(Guid id, DepartmentDto department)
    {
        var departmentDb = await _departmentRepository.GetDepartmentById(id);
        if (departmentDb == null) return NotFound();
        
        departmentDb.Name = department.Name;
        _departmentRepository.UpdateDepartment(departmentDb);
        await _departmentRepository.SaveAllAsync();
        var departmentDto = _mapper.Map<DepartmentDto>(departmentDb);
        return Ok(departmentDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        try
        {
            var departmentDb = await _departmentRepository.GetDepartmentById(id);
            if (departmentDb == null) NotFound();
            _departmentRepository.DeleteDepartment(departmentDb);
            await _departmentRepository.SaveAllAsync();
            var departmentDto = _mapper.Map<DepartmentDto>(departmentDb);
            return Ok(departmentDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
}