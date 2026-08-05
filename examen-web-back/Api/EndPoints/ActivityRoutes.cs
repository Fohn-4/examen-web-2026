using Core.Models;
using Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;

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

            group.MapPost("/", async ([FromBody] CreateActivityRequest request, IActivityUseCases useCases) =>
            {
                var activity = await useCases.Create(request);
                return Results.Created($"/api/activities/{activity.UUID}", activity);
            })
            .RequireAuthorization(policy => policy.RequireRole("Admin", "SuperAdmin"));

            group.MapDelete("/{uuid}", async (string uuid, IActivityUseCases useCases)=>
            {
                await useCases.Delete(uuid);
                return Results.NoContent();
            })
            .RequireAuthorization(policy => policy.RequireRole("Admin", "SuperAdmin"));

            return app;
        }
    }
}