using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Core.Models;
using Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace Api.EndPoints;

public static class UserRoutes
{
    private static string GenerateJwt(User user, IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: [
                new Claim(ClaimTypes.NameIdentifier, user.UUID),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                ..user.Roles.Select(r => new Claim(ClaimTypes.Role, r))
            ],
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public static WebApplication AddUserRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/users")
            .RequireAuthorization()
            .WithTags("Users");

        group.MapPost("/auth", async ([FromBody] AuthRequest request, IUserUseCases userUseCases, IConfiguration configuration) =>
        {
            var user = await userUseCases.Authenticate(request.Email, request.Password);
            return Results.Ok(new { token = GenerateJwt(user, configuration) });
        })
        .AllowAnonymous()
        .WithName("Auth");

        group.MapPost("/register", async ([FromBody] RegisterRequest request, IUserUseCases userUseCases, IConfiguration configuration) =>
        {
            var user = await userUseCases.Register(request);
            var token = GenerateJwt(user, configuration);
            return Results.Ok(new { token });
        }
        )
        .AllowAnonymous()
        .WithName("Register");

        return app;
    }


}