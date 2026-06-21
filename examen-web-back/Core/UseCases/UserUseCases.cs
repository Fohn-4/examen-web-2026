using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Core.IGateways;
using Core.UseCases.Abstractions;
using Core.Models;

namespace Core.UseCases
{
    public class UserUseCases : IUserUseCases
    {
        private readonly IUserGateway _gateway;

        public UserUseCases(IUserGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var result = await _gateway.GetByEmail(email);

            if (result is null) throw new UnauthorizedAccessException("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(password, result.Value.passwordHash)) throw new UnauthorizedAccessException("Invalid credentials");

            return result.Value.user;
        }

        public async Task<User> Register(RegisterRequest request)
        {
            var existingUser  = await _gateway.GetByEmail(request.Email);
            if(existingUser is not null) throw new InvalidOperationException("email already in use");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _gateway.Register(request, passwordHash);

            var result = await _gateway.GetByEmail(request.Email);
            return result!.Value.user;
        }
    }
}