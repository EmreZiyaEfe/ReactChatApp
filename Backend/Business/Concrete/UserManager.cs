using Backend.Business.Abstract;
using Backend.Data;
using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserManager(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            //Name control
            var exist = await _userRepository.GetAllAsync();
            if (exist.Any(u => u.NickName == user.NickName))
            {
                throw new Exception("This nickname is already used");
            }

            return await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
