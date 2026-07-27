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
    }
}