using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.UseCases.Abstractions;
using MySqlX.XDevAPI.Common;

namespace Api.EndPoints
{
    public static class EventRoutes
    {
        public static WebApplication AddEventRoutes(this WebApplication app)
        {
            var group = app.MapGroup("api/events").WithTags("Events");

            group.MapGet("/", async (HttpContext httpContext, IEventUseCases eventUsecase) =>
            {
                bool includePrivate = httpContext.User.Identity?.IsAuthenticated ?? false;

                var events = await eventUsecase.GetAll(includePrivate);

                return Results.Ok(events);
            });

            return app;
        }
    }
}