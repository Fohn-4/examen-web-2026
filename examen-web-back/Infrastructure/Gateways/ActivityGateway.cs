using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstraction;

namespace Infrastructure.Gateways
{
    public class ActivityGateway : IActivityGateway
    {
        private readonly IActivityRepository _repository;

        public ActivityGateway(IActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Activity>> GetAll(bool includeInactive)
        {
            var entities = await _repository.GetAll(includeInactive);

            return entities.Select(a => new Activity
            {
                UUID = a.UUID,
                Name = a.Name,
                Description = a.Description,
                IsActive = a.IsActive,
                Thumbnail = a.Thumbnail
            }).ToList();
        }

        public async Task<Activity> Create(CreateActivityRequest request)
        {
            var entity = await _repository.Create(request);

            return new Activity
            {
                UUID = entity.UUID,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
                Thumbnail = entity.Thumbnail
            };
        }

        public async Task Delete(string uuid)
        {
            await _repository.Delete(uuid);
        }
    }
}