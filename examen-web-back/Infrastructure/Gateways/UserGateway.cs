using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Infrastructure.Repositories.Abstraction;
using Core.IGateways;
using Infrastructure.Models;


namespace Infrastructure.Gateways;
    public class UserGateway : IUserGateway
    {
        private readonly IUserRepository _repository;

        public UserGateway(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<(User user, string passwordHash)?> GetByEmail(string email)
        {
            var result = await _repository.GetByEmail(email);

            if( result is null ) return null;
            
            var user = new User
            {
                UUID = result.UUID,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Roles = result.RoleNames?.Split(',').ToList() ?? []
            };

            return (user, result.PasswordHash);
        }

        public async Task Register(RegisterRequest request, string passwordHash)
        {
            await _repository.Register(request, passwordHash);
        }
    }
