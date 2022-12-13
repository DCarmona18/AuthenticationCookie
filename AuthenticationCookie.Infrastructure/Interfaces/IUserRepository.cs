using AuthenticationCookie.Infrastructure.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationCookie.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAndPassword(string username, string password);
        Task<bool> ValidateLastChanged(string lastChanged);
    }
}
