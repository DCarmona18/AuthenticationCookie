using AuthenticationCookie.Domain.Interfaces;
using AuthenticationCookie.Infrastructure.Context.Entities;
using AuthenticationCookie.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationCookie.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> Login(string username, string password)
        {
            return await _userRepository.GetUserByUsernameAndPassword(username, password);
        }
    }
}
