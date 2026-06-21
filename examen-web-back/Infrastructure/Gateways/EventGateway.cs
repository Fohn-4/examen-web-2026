using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstraction;

namespace Infrastructure.Gateways
{
    public class EventGateway : IEventGateway
    {
        private readonly IEventRepository _repository;

        public EventGateway(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Event>> GetAll(bool includePrivate)
        {
            var entities = await _repository.GetAll(includePrivate);

            return entities.Select(e => new Event
            {
                UUID = e.UUID,
                Name = e.Name,
                Location = e.Location,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                IsPublic = e.IsPublic,
                Thumbnail = e.Thumbnail
            }).ToList();
        }
    }
}