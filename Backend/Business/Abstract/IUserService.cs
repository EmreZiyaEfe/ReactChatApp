using Backend.Models;

namespace Backend.Business.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> CreateUserAsync(User user);
    }
}
