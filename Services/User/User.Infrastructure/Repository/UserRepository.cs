using Microsoft.EntityFrameworkCore;
using User.Application.Interfaces.IRepository;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<Domain.User> CreateUserAsync(Domain.User user)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.User?> GetUserByIdAsync(string userId)
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == userId && u.DeletedAt == null);
    }

    public async Task<Domain.User?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email && u.DeletedAt == null);
    }

    public Task<List<Domain.User>> ListUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUserAsync(Domain.User user)
    {
        _dbContext.Users.Update(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteUserAsync(Domain.User user)
    {
        _dbContext.Users.Remove(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}