using Business.Interfaces;
using Data;
public class UserService : IUserService
{
    readonly IUserRepository _repo;
    public UserService(IUserRepository repo) { _repo = repo; }

    public async Task<int> CreateAsync(DTO.User.CreateUserDTO dto)
    {
        if (await _repo.ExistsByEmailAsync(dto.Email))
            throw new InvalidOperationException($"Email '{dto.Email}' is already in use");

        var user = new Entities.UserEntity
        {
            Email = dto.Email.Trim().ToLower(),
            Role = dto.Role,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12)
        };

        return await _repo.CreateAsync(user);
    }

    public async Task<DTO.User.ReadUserDTO?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return null;

        return new DTO.User.ReadUserDTO
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public async Task<DTO.User.ReadUserDTO?> GetByEmailAsync(string email)
    {
        var user = await _repo.GetByEmailAsync(email.Trim().ToLower());
        if (user == null) return null;

        return new DTO.User.ReadUserDTO
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public async Task SoftDeleteAsync(int id, DTO.User.SoftUserDeleteDTO dto)
    {
        var user = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("User not found");

        if (user.IsDeleted)
            throw new InvalidOperationException("User is already deleted");

        if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
            throw new UnauthorizedAccessException("Current password is incorrect");

        user.IsDeleted = true;
        user.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(user);
    }

    public async Task UpdatePasswordAsync(int userId, DTO.User.UpdateUserPasswordDTO dto)
    {
        var user = await _repo.GetByIdAsync(userId)
            ?? throw new KeyNotFoundException("User not found");

        if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
            throw new UnauthorizedAccessException("Current password is incorrect");

        if (BCrypt.Net.BCrypt.Verify(dto.NewPassword, user.PasswordHash))
            throw new InvalidOperationException("New password cannot be the same as the current password");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword, workFactor: 12);
        user.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(user);
    }

    public async Task UpdateRoleAsync(int id, DTO.User.UpdateUserRoleDTO dto)
    {
        var user = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("User not found");

        user.Role = dto.NewRole;
        user.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(user);
    }

    public async Task UpdateEmailAsync(int userId, DTO.User.UpdateUserEmailDTO dto)
    {
        var normalizedEmail = dto.NewEmail.Trim().ToLower();

        var user = await _repo.GetByIdAsync(userId)
            ?? throw new KeyNotFoundException("User not found");

        if (user.IsDeleted)
            throw new InvalidOperationException("User account is deleted");

        if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
            throw new UnauthorizedAccessException("Password is incorrect");

        if (user.Email == normalizedEmail)
            throw new InvalidOperationException("New email cannot be the same as current email");

        var existing = await _repo.GetByEmailAsync(normalizedEmail);
        if (existing != null && existing.Id != userId)
            throw new InvalidOperationException("Email is already in use by another account");

        user.Email = normalizedEmail;
        user.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(user);
    }
}