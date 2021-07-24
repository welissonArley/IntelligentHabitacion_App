using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Homuai.Infrastructure.Migrations
{
    public static class Database
    {
        public static void EnsureDatabase(string connectionString, string databaseName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);
            using var connection = new MySqlConnection(connectionString);

            var records = connection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

            if (!records.Any())
                connection.Execute($"CREATE DATABASE {databaseName}");
        }
    }
}
