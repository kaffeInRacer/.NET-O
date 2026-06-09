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


    public async Task<Domain.User> CreateUserAsync(Domain.User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<Domain.User?> GetUserByIdAsync(string userId)
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .Include(u => u.Addresses)
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

    public async Task UpdateAddressesAsync(string userId, List<Domain.Addresses> addresses)
    {
        var existing = await _dbContext.Addresses
            .Where(x => x.UserId == userId)
            .ToListAsync();

        var existingMap = existing.ToDictionary(x => x.Id);
        var incomingIds = addresses.Select(x => x.Id).ToHashSet();

        // DELETE
        var toDelete = existing
            .Where(x => !incomingIds.Contains(x.Id));

        _dbContext.Addresses.RemoveRange(toDelete);

        // UPSERT
        foreach (var item in addresses)
        {
            if (existingMap.TryGetValue(item.Id, out var db))
            {
                db.VillageId = item.VillageId;
                db.Street = item.Street;
                db.UpdatedAt = DateTime.UtcNow;
                continue;
            }

            if (string.IsNullOrWhiteSpace(item.VillageId) ||
                string.IsNullOrWhiteSpace(item.Street))
                continue;

            item.UserId = userId;
            item.CreatedAt = DateTime.UtcNow;

            _dbContext.Addresses.Add(item);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteUserAsync(Domain.User user)
    {
        _dbContext.Users.Remove(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}