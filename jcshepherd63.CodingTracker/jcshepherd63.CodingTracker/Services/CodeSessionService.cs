using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using CodingTimeModel;
using DatabaseMethods;
using Menus;

namespace CodeSessionMethods;

public class CodeSessionService
{
    public static void AddCodingSession()
    {
        var codeTime = CodeSessionMenu.CodingTimeCreation();
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var addCmd = @"INSERT INTO CodingSessions (Date, StartTime, EndTime, Duration, ProjectId)
                       VALUES (@Date, @StartTime, @EndTime, @Duration, @ProjectId)";
            connection.Execute(addCmd, codeTime);
            connection.Close();
        }
    }

    public static void UpdateCodingSession()
    {
        CodingTime codeTime = new();
        CodeSessionController.OutputSessionListToTable();
        int Id = CodeSessionMenu.UpdateCodeGetID();
        codeTime = CodeSessionMenu.CodingTimeCreation();
        codeTime.Id = Id;

        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var updateCmd = @"UPDATE CodingSessions SET Date = @Date, StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration ,ProjectId = @ProjectId WHERE Id = @Id";
            connection.Execute(updateCmd, codeTime);
            connection.Close();
        }
    }

    public static void DeleteCodingSession()
    {
        CodingTime codeTime = new();
        CodeSessionController.OutputSessionListToTable();
        codeTime.Id = CodeSessionMenu.DeleteCodeGetID();
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var deleteCmd = @"DELETE FROM CodingSessions WHERE Id = @Id";
            connection.Execute(deleteCmd, codeTime);
            connection.Close();
        }
    }

    public static List<CodingTime> GetAllCodeSessions()
    {
        CodingTime codeTime = new();
        List<CodingTime> sessions;
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();

            var getCodingTimeCmd = @"SELECT * FROM CodingSessions";
            sessions = connection.Query<CodingTime>(getCodingTimeCmd, codeTime).ToList();

            connection.Close();
        }
        return sessions;
    }

    public static void GetCodeSessionDates()
    {
        var calendar = CodeSessionController.CodingCalendarOutput();
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var getSessionDatesCmd = "SELECT Date FROM codingSessions";
            var reader = connection.ExecuteReader(getSessionDatesCmd);
            while (reader.Read())
            {
                calendar.AddCalendarEvent(DateTime.Parse(reader.GetString(reader.GetOrdinal("Date"))), new Style(foreground: Color.Red, decoration: Decoration.Bold));
            }
            connection.Close();
        }
        AnsiConsole.Write(calendar);
    }

}