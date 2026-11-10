using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> AddUserAsync(User user);

        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
       
        Task<bool> UpdateUserAsync(User user);
    }
}
