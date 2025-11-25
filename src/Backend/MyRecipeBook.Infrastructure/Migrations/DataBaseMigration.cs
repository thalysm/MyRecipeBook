using Dapper;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace MyRecipeBook.Infrastructure.Migrations
{
    public static class DataBaseMigration
    {
        public static void Migrate(string connetionString, IServiceProvider serviceProvider)
        {
            EnsureDatabaseCreated(connetionString);
            MigrationDatabase(serviceProvider);
        }

        private static void EnsureDatabaseCreated(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.Database;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("databaseName", databaseName);

            var records = dbConnection.Query("SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @databaseName", parameters);

            if(records.Any() == false)
                dbConnection.Execute($"CREATE DATABASE `{databaseName}`;");

        }

        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.ListMigrations();

            runner.MigrateUp();
        }
    }
}
