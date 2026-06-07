namespace User.Domain;

public class Role : Base
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; }
    public List<RolePermission> RolePermissions { get; set; }
    public DateTime?  DeletedAt { get; set; }
}