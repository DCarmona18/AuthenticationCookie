using AuthenticationCookie.Infrastructure.Context;
using AuthenticationCookie.Infrastructure.Context.Entities;
using AuthenticationCookie.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationCookie.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByUsernameAndPassword(string username, string password) => 
            await _context.Users.FirstOrDefaultAsync(x => x.Email == username && x.Password == password);

        public async Task<bool> ValidateLastChanged(string lastChanged)
        {
            await _context.Users.FirstOrDefaultAsync(x => x.Email == lastChanged);
            return true;
        }
    }
}
