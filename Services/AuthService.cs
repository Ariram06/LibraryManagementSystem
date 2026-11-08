using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository.IRepository;
using LibraryManagementSystem.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(UserRegisterDto request)
        {
            // Talk to Repo layer to check if user exists
            var existingUser = await _authRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Email already registered." };
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role
            };

            // Talk to Repo layer to persist the data
            var success = await _authRepository.AddUserAsync(user);
            if (!success)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "An error occurred while saving the user." };
            }

            return new AuthResponseDto { IsSuccess = true, Message = "User registered successfully!" };
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto request)
        {
            // Talk to Repo layer to find the user
            var user = await _authRepository.GetUserByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid email or password." };
            }

            string token = GenerateJwtToken(user);
            return new AuthResponseDto { IsSuccess = true, Message = "Login successful.", Token = token };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:Issuer"],
                audience: _configuration["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
