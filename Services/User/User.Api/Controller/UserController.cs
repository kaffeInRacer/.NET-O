using Microsoft.AspNetCore.Mvc;
using Shared.Response;
using User.Api.DTOs;

namespace User.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
   [HttpPost]
   [Consumes("multipart/form-data")]
   public async Task<IActionResult> Create([FromForm] CreateUserRequest request)
   {
      return Ok(request);
   }

}