using User.Application.DTOs;

namespace User.Application.Interfaces.IUseCase;

public interface IUserUseCase
{
    Task<Domain.User?> GetUserById(string id);
    Task<bool> CreateUserAsync(CreateUserDto dto);
    Task<bool> UpdateUserAsync(string id, UpdateUserDto dto);
}
