using Core.Models;

namespace Core.IGateways
{
    public interface IEventGateway
    {
        Task<List<Event>> GetAll(bool includePrivate);
        Task Delete(string uuid);
    }
}