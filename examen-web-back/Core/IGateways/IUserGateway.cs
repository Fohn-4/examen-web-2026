using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.IGateways
{
    public interface IUserGateway
    {
        Task<(User user, string passwordHash)?> GetByEmail(string email);
        Task Register(RegisterRequest request, string passwordHash);
    }
}