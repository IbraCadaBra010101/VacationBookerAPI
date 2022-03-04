using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VacationBookerAPI.Entities;

namespace VacationBookerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
 

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("Register")]
    // Post: /api/user/register
    public async Task<IActionResult> PostApplication(UserModel model)
    {
        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        
        try
        {
            var result = await _userManager.CreateAsync(user, model.Password);
            return Ok(result);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}