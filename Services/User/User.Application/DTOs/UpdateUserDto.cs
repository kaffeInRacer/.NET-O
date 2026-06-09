using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User.Application.DTOs;

public class UpdateUserDto
{
    [FromForm(Name = "username")]
    public string? Username    { get; set; }
    [FromForm(Name = "password")]
    public string? Password    { get; set; }
    [FromForm(Name = "email")]
    public string? Email       { get; set; }
    [FromForm(Name = "phone_number")]
    public string? PhoneNumber { get; set; }
    [FromForm(Name = "gender")]
    public string? Gender      { get; set; }
    [FromForm(Name = "image")]
    public IFormFile? Image    { get; set; }
    [FromForm(Name = "addresses")]
    public List<UpdateAddressDto> Addresses { get; set; } = new();
}

public class UpdateAddressDto
{
    [FromForm(Name = "id")]
    public string? Id        { get; set; }
    [FromForm(Name = "village_id")]
    public string  VillageId { get; set; } = string.Empty;
    [FromForm(Name = "street")]
    public string  Street    { get; set; } = string.Empty;
}
