using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Models;

namespace AuthService.Domain.Interfaces
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {
        Task<AppUser> GetUserByUserName(string userName);
    }
}

