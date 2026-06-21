using System;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstraction
{
    public interface IEventRepository
    {
        Task<List<EventEntity>> GetAll(bool includePrivate);
    }
}