using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace DatabaseMethods;

public class _DatabaseCreation
{
    public static string GetConnection()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        return connectionString;
    }

    public static void dbTableCreate()
    {
        var connectionString = GetConnection();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = @"CREATE TABLE IF NOT EXISTS CodingSessions
                            (Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            Date TEXT, StartTime TEXT, EndTime TEXT, Duration TEXT, ProjectId INTEGER);

                            CREATE TABLE IF NOT EXISTS Projects(
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT);";

            connection.Execute(tableCmd);
            connection.Close();
        }
    }
}