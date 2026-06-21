using Core.Models;

namespace Core.UseCases.Abstractions
{
    public interface IEventUseCases
    {
        Task<List<Event>> GetAll(bool includePrivate);
    }
}