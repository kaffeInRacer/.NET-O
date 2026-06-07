namespace User.Application.Interfaces.IRepository;

public interface IUserRepository
{
    Task<Domain.User> CreateUserAsync(Domain.User user);
    Task<Domain.User?> GetUserByIdAsync(string userId);
    Task<Domain.User?> GetUserByEmailAsync(string email);
    Task<List<Domain.User>> ListUsersAsync();
    Task<bool> UpdateUserAsync(Domain.User user);
    Task<bool> DeleteUserAsync(Domain.User userId);
}