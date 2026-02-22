using CodingTrackerEnums;
using Spectre.Console;
using System.Collections.Generic;
using Menus;
using ProjectModel;

namespace ProjectMethods;

public class ProjectController
{
    public static void ProjectRouter(Enums.ProjectMenuOptions selection)
    {
        switch (selection)
        {
            case Enums.ProjectMenuOptions.Add_Project:
                {
                    var project = ProjectMenu.NewProjectCreation();
                    ProjectService.AddProject(project);
                    break;
                }
            case Enums.ProjectMenuOptions.Update_Project:
                {
                    int id = ProjectMenu.UpdateProjectGetId();
                    var project = ProjectMenu.NewProjectCreation();
                    project.Id = id;
                    ProjectService.UpdateProject(project);
                    break;
                }
            case Enums.ProjectMenuOptions.Delete_Project:
                {
                    OutputProjectListToTable();
                    var project = ProjectMenu.DeleteProjectById();
                    ProjectService.DeleteProject(project);
                    break;
                }
            case Enums.ProjectMenuOptions.View_Projects:
                {
                    OutputProjectListToTable();
                    break;
                }
        }
    }

    private static Table ProjectTable()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.DarkCyan)
            .Title("[yellow bold]Projects[/]");

        table.AddColumn("[italic]Project ID[/]");
        table.AddColumn("[italic]Project Name[/]");
        return table;
    }

    public static void OutputProjectListToTable()
    {
        List<Project> projects = ProjectService.GetAllProjects();
        var table = ProjectTable();

        foreach (Project project in projects)
        {
            table.AddRow(
                project.Id.ToString(),
                project.Name);
        }
        AnsiConsole.Write(table);
    }
}