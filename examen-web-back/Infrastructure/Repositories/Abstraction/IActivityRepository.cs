using Core.Models;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstraction
{
    public interface IActivityRepository
    {
        Task<List<ActivityEntity>> GetAll(bool includeInactive);
        Task<ActivityEntity> Create(CreateActivityRequest request);
    }
}