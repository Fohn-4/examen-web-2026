using Core.Models;

namespace Infrastructure.Repositories.Abstraction
{
    public interface IUserRepository
    {
        Task<Models.UserEntity?> GetByEmail(string email);
        Task Register(RegisterRequest request, string passwordHash);
    }
}