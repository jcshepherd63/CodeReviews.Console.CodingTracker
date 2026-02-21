using Spectre.Console;

public class CodeSessionMenu
{
    private static DateTime CodeDateSelectionMenu()
    {
        Console.Clear();
        DateTime today;
        var CodeTimeMenuOutput = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[yellow]How would you like to perform date entry?[/]")
            .AddChoices("Use today's date", "Custom date entry"));

        if (CodeTimeMenuOutput == "Use today's date")
        {
            today = DateTime.Now;
            Console.WriteLine($"{today:d}");
        }
        else
        {
            today = AnsiConsole.Ask<DateTime>("[yellow]What day did you work on coding? (Required Format MM-dd-yyyy)[/]");
            Console.WriteLine($"{today:d}");
        }

        return today;
    }

    private static string TimeFormatSelection()
    {
        var CodeTimeMenuOutput = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow bold]Session Timer Screen[/]")
                .AddChoices("Start Session Now", "Custom Start Time"));
        return CodeTimeMenuOutput;
    }

    private static DateTime SessionStartTimeSelectionMenu()
    {
        Console.Clear();
        DateTime startTime = DateTime.Now;
        var CodeTimeMenuOutput = TimeFormatSelection();

        if (CodeTimeMenuOutput == "Start Session Now")
        {
            startTime = DateTime.Now;
        }
        else
        {
            startTime = UserSessionStartTimeEntry();
        }

        return startTime;
    }

    private static DateTime UserSessionStartTimeEntry()
    {
        Console.Clear();
        DateTime startTime = AnsiConsole.Ask<DateTime>("[yellow bold]What time did you start your coding session? (Format 24Hour HH:MM:SS)[/]");
        return startTime;
    }

    private static DateTime SessionEndTimeSelectionMenu()
    {
        Console.Clear();
        var result = AnsiConsole.Ask<string>("[yellow]Please enter 'Stop' to end the timed session or 'Custom' to enter the endtime manually[/]").ToLower();
        if(result == "stop")
        {
            return DateTime.Now;
        }
        else if(result == "custom")
        {
            return UserSessionEndTimeEntry();
        }
        else
        {
            AnsiConsole.MarkupLine("[red italic]That was not a valid entry. Please try again. \n[/]");
            return SessionEndTimeSelectionMenu();
        }
    }

    private static DateTime UserSessionEndTimeEntry()
    {
        Console.Clear();
        DateTime endTime = AnsiConsole.Ask<DateTime>("[yellow bold]What time did you end your coding session? (Format 24Hour HH:MM:SS)[/]");
        return endTime;
    }

    public static Project CodeProjectSelectionMenu()
    {
        Console.Clear();
        Project project = null;
        var projects = ProjectService.GetAllProjects();
        var prompt = new SelectionPrompt<string>()
            .Title("[yellow]What project are/were you working on[/]")
            .AddChoiceGroup("New Project");

        foreach(Project proj in projects)
        {
            prompt.AddChoice(proj.ToString());
        }

        var selection = AnsiConsole.Prompt(prompt);

        if (selection == "New Project")
        {
            project = ProjectMenu.NewProjectCreation();
            ProjectService.AddProject(project);
        }
        else
        {
            foreach(Project proj in projects)
            {
                if(selection == proj.Name)
                {
                    project = proj;
                }
            }
        }

        return project;
    }


    public static int UpdateCodeGetID()
    {
        var UpdateSessionByID = AnsiConsole.Ask<int>("[yellow]Which session do you want to update?[/]");
        return UpdateSessionByID;
    }

    public static int DeleteCodeGetID()
    {
        var DeleteSessionByID = AnsiConsole.Ask<int>("[red bold]Which session do you want to delete?[/]");
        return DeleteSessionByID;
    }

    public static CodingTime CodingTimeCreation()
    {
        var date = CodeSessionMenu.CodeDateSelectionMenu();
        var startTime = CodeSessionMenu.SessionStartTimeSelectionMenu();
        var endTime = CodeSessionMenu.SessionEndTimeSelectionMenu();
        var duration = endTime - startTime;
        var projectId = ProjectService.GetProjectId();

        CodingTime codeTime = new()
        {
            Date = date,
            StartTime = startTime,
            EndTime = endTime,
            ProjectId = Convert.ToInt32(projectId)
        };

        return codeTime;
    }
}

