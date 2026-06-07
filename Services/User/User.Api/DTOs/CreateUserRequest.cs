using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace User.Api.DTOs;
public class CreateUserRequest
{
    [FromForm(Name = "username")]
    public string Username    { get; set; } = string.Empty;
    [FromForm(Name = "password")]
    public string Password    { get; set; } = string.Empty;
    [FromForm(Name = "email")]
    public string Email       { get; set; } = string.Empty;
    [FromForm(Name = "phone")]
    public string PhoneNumber { get; set; } = string.Empty;
    [FromForm(Name = "gender")]
    public string Gender      { get; set; } = string.Empty;
    [FromForm(Name = "balance")]
    public decimal Balance    { get; set; }
    [FromForm(Name = "role")]
    public string RoleId      { get; set; } = string.Empty;
    [FromForm(Name = "image")]
    public IFormFile? ProfileImage { get; set; }
    [FromForm(Name = "address")]
    public List<CreateAddressRequest> Addresses { get; set; } = new();
}

public class CreateAddressRequest
{
    public string Province { get; set; } = string.Empty;
    public string City     { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string ZipCode  { get; set; } = string.Empty;
    public string Street   { get; set; } = string.Empty;
}
