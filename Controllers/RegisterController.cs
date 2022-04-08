using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VacationBookerAPI.Entities;

namespace VacationBookerAPI.Controllers;

[ApiController]
[Route("api/Register")]
public class RegisterController : ControllerBase
{
    private readonly UserManager<Register> _userManager;
    private SignInManager<Register> _signInManager;
    private readonly IMapper _mapper;

    public RegisterController(UserManager<Register> userManager, SignInManager<Register> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(RegisterDto registerDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var UserDb = new Register
        {
            Email = registerDto.Email,
            Password = registerDto.Password,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };

        await _userManager.CreateAsync(UserDb, UserDb.Password);
        var UserDto = _mapper.Map<Register, RegisterDto>(UserDb);
        
        return Ok(UserDto);
    }
    
   
}