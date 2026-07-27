using Core.Models;

namespace Core.IGateways
{
    public interface IActivityGateway
    {
        Task<List<Activity>> GetAll(bool includeInactive);
    }
}