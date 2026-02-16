
using Microsoft.Data.Sqlite;
using Dapper;

public class ProjectService
{
    public static void AddProject(Project project)
    {
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var addProjectCmd = @"INSERT INTO projects (Name)
                                VALUES (@Name)";
            connection.Execute(addProjectCmd, project);
            connection.Close();
        }
    }

    public static void UpdateProject(Project project)
    {
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var updateByIdCmd = "UPDATE Projects SET Name = @Name WHERE Id = @Id";
            connection.Execute(updateByIdCmd, project);
            connection.Close();
        }
    }

    public static void DeleteProject(Project project)
    {
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var DeleteProjectCmd = @"DELETE FROM projects WHERE Id = @Id";
            connection.Execute(DeleteProjectCmd, project);
            connection.Close();
        }
    }

    public static int GetProjectId()
    {
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            Project project = CodeSessionMenu.CodeProjectSelectionMenu();
            var getProjectIdCmd = $"SELECT Id from Projects WHERE Name = '{project.Name}'";
            var projectID = connection.QuerySingle<int>(getProjectIdCmd);
            connection.Close();

            return projectID;
        }
    }

    public static List<Project> GetAllProjects()
    {
        using (var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var getProjectsCmd = "SELECT * From projects";
            var projects = connection.Query<Project>(getProjectsCmd).ToList();
            connection.Close();
            return projects;
        }
    }

    public static string GetProjectNameFromId(CodingTime session)
    {
        using(var connection = new SqliteConnection(_DatabaseCreation.GetConnection()))
        {
            connection.Open();
            var getProjectNameById = "SELECT Name FROM Projects WHERE Id = @ProjectId";
            var projectName = connection.QuerySingle<string>(getProjectNameById, session);
            connection.Close();
            return projectName;
        }
    }
}
