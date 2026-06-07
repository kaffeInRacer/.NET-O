namespace User.Domain;

public class RolePermission : Base
{
    public string RoleId { get; set; }
    public string PermissionId { get; set; }
    public Role Role { get; set; }
    public Permission Permission { get; set; }
    public DateTime? DeletedAt { get; set; }
}