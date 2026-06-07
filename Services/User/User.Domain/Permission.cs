namespace User.Domain;

public class Permission : Base
{
    public string Name { get; set; } = string.Empty;       // "user:read"
    public string Resource { get; set; } = string.Empty;   // "user"
    public string Action { get; set; } = string.Empty;     // "read"
    public DateTime? DeletedAt { get; set; }
    public List<RolePermission> RolePermissions { get; set; } = new();
}