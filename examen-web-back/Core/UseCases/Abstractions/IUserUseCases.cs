using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.UseCases.Abstractions
{
    public interface IUserUseCases
    {
        Task<User> Authenticate(string email, string password);
        Task<User> Register(RegisterRequest request);
    }
}