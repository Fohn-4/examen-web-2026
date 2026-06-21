using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstraction;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Prng;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<Models.UserEntity?> GetByEmail(string email)
    {
        const string sql = @"
            SELECT 
                u.UUID,
                u.email AS Email,
                u.first_name AS FirstName,
                u.last_name AS LastName,
                u.password_hash AS PasswordHash,
                GROUP_CONCAT(r.name) AS RoleNames
            FROM app_user u
            LEFT JOIN app_user_role ur ON u.UUID = ur.app_user_UUID
            LEFT JOIN role r ON ur.role_UUID = r.UUID
            WHERE u.email = @Email
            GROUP BY u.UUID";

        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Models.UserEntity>(sql, new { Email = email });
    }

    public async Task Register(RegisterRequest request, string passwordHash)
    {
        const string sql = @"
        INSERT INTO app_user (email, first_name, last_name, password_hash)
        VALUES (@Email, @FirstName, @LastName, @PasswordHash)";

        using var connection = new MySqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new
        {
            request.Email,
            request.FirstName,
            request.LastName,
            PasswordHash = passwordHash
        });
    }
}