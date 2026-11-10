using LibraryManagementSystem.DTOs;

namespace LibraryManagementSystem.Services.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserRegisterDto request);
        Task<AuthResponseDto> LoginAsync(UserLoginDto request);

        Task<AuthResponseDto> RefreshTokenAsync(AuthResponseDto request);
    }
}
