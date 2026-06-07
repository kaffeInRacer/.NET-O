namespace User.Domain;

public class User : Base
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string? Image { get; set; }
    public decimal Balance { get; set; }
    public string RoleId { get; set; } = string.Empty;
    public Role Role { get; set; } = null!;
    public List<RefreshToken> RefreshTokens { get; set; } = new();
    public List<Addresses> Addresses { get; set; } = new();
    public DateTime? DeletedAt { get; set; }
}
