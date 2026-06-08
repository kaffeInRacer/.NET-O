namespace User.Api.DTOs;

public class UpdateUserRequest
{
    public string?    Username     { get; set; }
    public string?    Password     { get; set; }
    public string?    Email        { get; set; }
    public string?    PhoneNumber  { get; set; }
    public string?    Gender       { get; set; }
    public IFormFile? ProfileImage { get; set; }

    // Full replacement strategy:
    // - Addresses with Id    → update existing
    // - Addresses without Id → insert new
    // - DB addresses absent from this list → deleted by service
    public List<UpdateAddressRequest> Addresses { get; set; } = new();
}

public class UpdateAddressRequest
{
    public string?  Id        { get; set; }
    public string   VillageId { get; set; } = string.Empty;
    public string   Street    { get; set; } = string.Empty;
}