using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User.Application.DTOs;

public class CreateUserDto
{
    [FromForm(Name = "username")] 
    [JsonPropertyName("username")]
    public string  Username    { get; set; } = string.Empty;
    [FromForm(Name = "password")] 
    [JsonPropertyName("password")]
    public string  Password    { get; set; } = string.Empty;
    [FromForm(Name = "email")] 
    [JsonPropertyName("email")]
    public string  Email       { get; set; } = string.Empty;
    [FromForm(Name = "phone_number")]
    [JsonPropertyName("phone_number")]
    public string  PhoneNumber { get; set; } = string.Empty;
    [FromForm(Name = "gender")]
    [JsonPropertyName("gender")]
    public string  Gender      { get; set; } = string.Empty;
    [FromForm(Name = "balance")]
    [JsonPropertyName("balance")]
    public decimal Balance     { get; set; }
    [FromForm(Name = "role_id")]
    [JsonPropertyName("role_id")]
    public string  RoleId      { get; set; } = string.Empty;
    [FromForm(Name = "image")]
    [JsonPropertyName("image")]
    public IFormFile? Image       { get; set; }
    [FromForm(Name = "address")]
    [JsonPropertyName("address")]
    public List<AddressDto> Addresses { get; set; } = new();
}

public class AddressDto
{
    [FromForm(Name = "village_id")]
    [JsonPropertyName("village_id")]
    public string VillageId { get; set; } = string.Empty;
    [FromForm(Name = "street")]
    [JsonPropertyName("street")]
    public string Street    { get; set; } = string.Empty;
}
