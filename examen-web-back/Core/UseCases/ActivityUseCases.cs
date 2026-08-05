using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases
{
    public class ActivityUseCases : IActivityUseCases
    {
        private readonly IActivityGateway _gateway;
        public ActivityUseCases(IActivityGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<Activity>> GetAll(bool includeInactive)
        {
            return await _gateway.GetAll(includeInactive);
        }

        public async Task<Activity> Create(CreateActivityRequest request)
        {
            return await _gateway.Create(request);
        }

        public async Task Delete(string uuid)
        {
            await _gateway.Delete(uuid);
        }

        public async Task<Activity> Update(string uuid, UpdateActivityRequest request)
        {
            return await _gateway.Update(uuid, request);
        }
    }
}