using DTO.Auth;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDTO?> LoginAsync(LoginRequestDTO dto);
        Task<TokenResponseDTO?> RefreshTokenAsync(RefreshRequestDTO dto);
        Task LogoutAsync(int userId, string refreshToken);
    }
}
