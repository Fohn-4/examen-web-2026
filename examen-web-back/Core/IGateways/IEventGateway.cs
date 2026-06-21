using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.IGateways
{
    public interface IEventGateway
    {
        Task<List<Event>> GetAll(bool includePrivate);
    }
}