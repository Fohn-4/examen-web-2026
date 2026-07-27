using Core.UseCases.Abstractions;

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