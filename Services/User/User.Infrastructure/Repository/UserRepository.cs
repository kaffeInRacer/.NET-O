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
        var existingInDb = await _dbContext.Addresses
            .Where(a => a.UserId == userId)
            .ToListAsync();

        var existingIds = existingInDb.Select(a => a.Id).ToHashSet();
        var incomingIds = addresses.Select(a => a.Id).ToHashSet();

        // DELETE: ada di DB tapi tidak di incoming
        var toDelete = existingInDb.Where(a => !incomingIds.Contains(a.Id)).ToList();
        _dbContext.Addresses.RemoveRange(toDelete);

        foreach (var incoming in addresses)
        {
            if (existingIds.Contains(incoming.Id))
            {
                // UPDATE: id dikenal di DB
                var target = existingInDb.First(a => a.Id == incoming.Id);
                target.VillageId = incoming.VillageId;
                target.Street    = incoming.Street;
                target.UpdatedAt = DateTime.UtcNow;
            }
            else if (!string.IsNullOrWhiteSpace(incoming.VillageId) && !string.IsNullOrWhiteSpace(incoming.Street))
            {
                // INSERT: id tidak dikenal di DB + field tidak kosong
                incoming.UserId    = userId;
                incoming.CreatedAt = DateTime.UtcNow;
                await _dbContext.Addresses.AddAsync(incoming);
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteUserAsync(Domain.User user)
    {
        _dbContext.Users.Remove(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}