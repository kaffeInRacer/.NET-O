namespace User.Api.DTOs;

public class CreateAddressRequest
{
    public string VillageId { get; set; } = string.Empty;
    public string Street    { get; set; } = string.Empty;
}
