using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories.Abstraction;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly string _connectionString;
        public EventRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<List<Models.EventEntity>> GetAll(bool includePrivate)
        {
            const string sql = @"
            SELECT
                e.UUID,
                e.name AS Name,
                e.location AS Location,
                e.start_date AS StartDate,
                e.end_date AS EndDate,
                e.is_public AS IsPublic,
                i.image_url AS Thumbnail
            FROM event e
            LEFT JOIN image i ON e.thumbnail = i.UUID
            WHERE (@IncludePrivate = TRUE OR e.is_public = TRUE)
            ";

            using var connection = new MySqlConnection(_connectionString);
            var result = await connection.QueryAsync<Models.EventEntity>(sql, new { IncludePrivate = includePrivate });
            return result.ToList();
        }

    }
}