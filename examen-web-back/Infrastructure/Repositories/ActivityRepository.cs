using Core.Models;
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

        public async Task<ActivityEntity> Create(CreateActivityRequest request)
        {
            var uuid = Guid.NewGuid().ToString("N");

            const string sql = @"
            INSERT INTO activity (UUID, name, description, is_active)
            VALUES (@uuid, @Name, @Description, @IsActive);
            ";

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, new
            {
                uuid,
                request.Name,
                request.Description,
                request.IsActive
            });

            return new ActivityEntity {
                UUID = uuid,
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                Thumbnail = null
            };
        }

        public async Task Delete(string uuid)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string sql = @"
            DELETE FROM activity WHERE UUID = @uuid;
            ";

            await connection.ExecuteAsync(sql, new {uuid});
        }
    }
}