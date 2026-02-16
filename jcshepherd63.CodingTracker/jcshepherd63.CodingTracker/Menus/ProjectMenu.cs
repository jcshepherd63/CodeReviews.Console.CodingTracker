using jcshepherd63.CodingTracker.Models;
using Spectre.Console;

public class ProjectMenu
{
    public static Enums.ProjectMenuOptions ProjectMenuUserInteraction()
    {
        Console.Clear();
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<Enums.ProjectMenuOptions>()
            .Title("What would you like to do in the project menu?")
            .AddChoices(Enum.GetValues<Enums.ProjectMenuOptions>()));

        return selection;
    }


    public static Project NewProjectCreation()
    {
        var newProjectName = AnsiConsole.Ask<string>("[yellow]What is the name of the project?[/]");
        var project = new Project(newProjectName);
        return project;

    }

    public static int UpdateProjectGetId()
    {
        List<Project> projects = ProjectService.GetAllProjects();
        var prompt = new SelectionPrompt<Project>()
            .Title("[yellow bold]Which project would you like to update?[/]");

        foreach (Project proj in projects)
        {
            prompt.AddChoice(proj);
        }
        var result = AnsiConsole.Prompt(prompt);

        return result.Id;
    }

    public static Project DeleteProjectById()
    {
        var projects = ProjectService.GetAllProjects();
        Project projectToDelete = null;
        var DeleteProjByID = AnsiConsole.Ask<int>("[red bold]Please enter the ID of the project you want to delete.[/]");
        foreach(var proj in projects)
        {
            if(proj.Id == DeleteProjByID)
            {
                projectToDelete = proj;
                break;
            }
        }
        return projectToDelete;
    }

}