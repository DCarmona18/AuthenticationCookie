using AuthenticationCookie.Infrastructure.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationCookie.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User?> Login(string username, string password);
    }
}
