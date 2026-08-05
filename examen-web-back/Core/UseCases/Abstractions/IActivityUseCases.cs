using Core.Models;

namespace Core.UseCases.Abstractions
{
    public interface IActivityUseCases
    {
        Task<List<Activity>> GetAll(bool includeInactive);
        Task<Activity> Create(CreateActivityRequest request);
        Task Delete(string uuid);
        Task<Activity> Update(string uuid, UpdateActivityRequest request);
    }
}