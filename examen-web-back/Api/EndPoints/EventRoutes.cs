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

            group.MapDelete("/{uuid}", async (string uuid, IEventUseCases useCases) =>
            {
                await useCases.Delete(uuid);
                return Results.NoContent();
            })
            .RequireAuthorization( policy => policy.RequireRole("Admin", "SuperAdmin"));

            return app;
        }
    }
}