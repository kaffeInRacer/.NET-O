namespace User.Domain;

public class Addresses : Base
{
    public string UserId    { get; set; } = string.Empty;
    public string VillageId { get; set; } = string.Empty;
    public string Street    { get; set; } = string.Empty;
    public User User        { get; set; } = null!;
}