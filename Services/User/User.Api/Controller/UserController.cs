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

   [HttpPut("{id}")]
   [Consumes("multipart/form-data")]
   public async Task<IActionResult> Update(string id, [FromForm] UpdateUserRequest request)
   {
      return Ok(ApiResponse<object>.Success(new
      {
         Id = id,
         request.Username,
         request.Password,
         request.Email,
         request.PhoneNumber,
         request.Gender,
         ProfileImage = request.ProfileImage is not null
            ? new { request.ProfileImage.FileName, request.ProfileImage.ContentType, request.ProfileImage.Length }
            : null,
         Addresses = request.Addresses.Select(a => new
         {
            a.Id,
            a.VillageId,
            a.Street
         })
      }));
   }

}