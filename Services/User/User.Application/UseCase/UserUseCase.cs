using Microsoft.Extensions.Logging;
using User.Application.DTOs;
using User.Application.Interfaces.IRepository;
using User.Application.Interfaces.IS3Storage;
using User.Application.Interfaces.IUseCase;
using User.Domain;

namespace User.Application.UseCase;

public class UserUseCase : IUserUseCase
{
    private readonly IUserRepository    _userRepository;
    private readonly IS3StorageService  _s3StorageService;
    private readonly ILogger<UserUseCase> _logger;

    public UserUseCase(
        IUserRepository userRepository,
        IS3StorageService s3StorageService,
        ILogger<UserUseCase> logger)
    {
        _userRepository   = userRepository;
        _s3StorageService = s3StorageService;
        _logger           = logger;
    }

    public Task<Domain.User?> GetUserById(string id)
        => _userRepository.GetUserByIdAsync(id);
    

    public async Task<bool> CreateUserAsync(CreateUserDto dto)
    {
        try
        {
            var user = new Domain.User
            {
                Username    = dto.Username,
                Password    = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Email       = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Gender      = dto.Gender,
                Balance     = dto.Balance,
                RoleId      = dto.RoleId,
                CreatedAt   = DateTime.UtcNow,
                Addresses   = dto.Addresses.Select(a => new Addresses
                {
                    VillageId = a.VillageId,
                    Street    = a.Street,
                    CreatedAt = DateTime.UtcNow
                }).ToList()
            };

            if (dto.Image is not null)
            {
                user.Image = await _s3StorageService.UploadAsync(
                    dto.Image.OpenReadStream(),
                    dto.Image.FileName,
                    dto.Image.ContentType);
            }

            await _userRepository.CreateUserAsync(user);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create user {Email}", dto.Email);
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(string id, UpdateUserDto dto)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user is null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Username)) user.Username    = dto.Username;
            if (!string.IsNullOrWhiteSpace(dto.Password)) user.Password    = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email       = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber)) user.PhoneNumber = dto.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(dto.Gender))  user.Gender      = dto.Gender;
            user.UpdatedAt = DateTime.UtcNow;

            if (dto.Image is not null)
            {
                if (user.Image is not null) await _s3StorageService.DeleteAsync(user.Image);

                user.Image = await _s3StorageService.UploadAsync(
                    dto.Image.OpenReadStream(),
                    dto.Image.FileName,
                    dto.Image.ContentType);
            }

            await _userRepository.UpdateUserAsync(user);

            var addresses = dto.Addresses.Select(a => new Addresses
            {
                Id        = a.Id ?? Guid.NewGuid().ToString(),
                VillageId = a.VillageId,
                Street    = a.Street,
                UserId    = id
            }).ToList();

            await _userRepository.UpdateAddressesAsync(id, addresses);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to update user {Id}", id);
            return false;
        }
    }
}
