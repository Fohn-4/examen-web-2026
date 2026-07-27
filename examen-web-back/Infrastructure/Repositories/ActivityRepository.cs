using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstraction;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly string _connectionString;

        public ActivityRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found");
        }

        public async Task<List<ActivityEntity>> GetAll(bool includeInactive)
        {
            const string sql = @"
            SELECT 
                a.UUID,
                a.name AS Name,
                a.description AS Description,
                a.is_active AS IsActive,
                i.image_url AS Thumbnail
            FROM activity a
            LEFT JOIN image i ON a.thumbnail = i.UUID
            WHERE (@includeInactive = TRUE OR a.is_active = TRUE )
            ";

            using var connection = new MySqlConnection(_connectionString);
            var result = await connection.QueryAsync<ActivityEntity>(sql, new { includeInactive });
            return result.ToList();
        }
    }
}