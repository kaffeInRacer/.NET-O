using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.Response;
using User.Application.DTOs;
using User.Application.Interfaces.IUseCase;

namespace User.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserUseCase _userUseCase;

    public UserController(IUserUseCase userUseCase)
    {
        _userUseCase = userUseCase;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _userUseCase.GetUserById(id);

        if (user is null)
            return NotFound(ApiResponse<object>.Fail("User not found"));

        return Ok(ApiResponse<object>.Success(new
        {
            user.Id,
            user.Username,
            user.Email,
            user.PhoneNumber,
            user.Gender,
            user.Balance,
            user.Image,
            user.RoleId,
            Role      = user.Role?.Name,
            Addresses = user.Addresses.Select(a => new
            {
                a.Id,
                a.VillageId,
                a.Street
            }),
            user.CreatedAt,
            user.UpdatedAt
        }));
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateUserDto dto)
    {
        await _userUseCase.CreateUserAsync(dto);
        return Ok(ApiResponse<object>.Success(null));
    }

    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(string id, [FromForm] UpdateUserDto dto)
    {
        var result = await _userUseCase.UpdateUserAsync(id, dto);
        
        if (!result)
            return NotFound(ApiResponse<object>.Fail("User not found"));

        return Ok(ApiResponse<object>.Success(null));
    }
}
