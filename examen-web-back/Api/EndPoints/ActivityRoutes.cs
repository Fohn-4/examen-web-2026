using Core.UseCases.Abstractions;

namespace Api.EndPoints
{
    public static class ActivityRoutes
    {
        public static WebApplication AddActivityRoutes(this WebApplication app)
        {
            var group = app.MapGroup("api/activities").WithTags("Activity");

            group.MapGet("/", async (HttpContext httpContext, IActivityUseCases activityUsecases) =>
            {
                bool includeInactive =
                    httpContext.User.IsInRole("Effective")
                    || httpContext.User.IsInRole("Admin")
                    || httpContext.User.IsInRole("SuperAdmin");

                var activities = await activityUsecases.GetAll(includeInactive);

                return Results.Ok(activities);
            });

            return app;
        }
    }
}