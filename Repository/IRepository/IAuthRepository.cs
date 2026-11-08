using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> AddUserAsync(User user);
    }
}
