
using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases
{
    public class EventUseCases : IEventUseCases
    {

        private readonly IEventGateway _gateway;
        public EventUseCases(IEventGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<Event>> GetAll(bool includePrivate)
        {
            return await _gateway.GetAll(includePrivate);
        }
    }
}