using Spectre.Console;
using CodingTrackerEnums;
using CodeSessionMethods;
using ProjectMethods; 

namespace Menus;

public class MainMenu
{
    public static void Menu()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[yellow]Coding Tracker Application:\n[/]");

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MainMenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(Enum.GetValues<Enums.MainMenuOptions>()));

            switch (selection)
            {
                case Enums.MainMenuOptions.Add_Coding_Session:
                    CodeSessionService.AddCodingSession();
                    AnsiConsole.MarkupLine("[red italic]Session Added.\nPress Any Key to Return to the Main Menu[/]");
                    Console.ReadKey();
                    break;

                case Enums.MainMenuOptions.Update_Coding_Session:
                    CodeSessionService.UpdateCodingSession();
                    AnsiConsole.MarkupLine("[red]Session Updated.\nPress Any Key to Return to the Main Menu[/]");
                    Console.ReadKey();
                    break;

                case Enums.MainMenuOptions.Delete_Coding_Session:
                    CodeSessionService.DeleteCodingSession();
                    AnsiConsole.MarkupLine("[red bold]Session Deleted.\nPress Any Key to Return to the Main Menu[/]");
                    Console.ReadKey();
                    break;

                case Enums.MainMenuOptions.View_Coding_Session:
                    CodeSessionController.OutputSessionListToTable();
                    AnsiConsole.MarkupLine("[red italic]Press Any Key to Return to the Main Menu[/]");
                    Console.ReadKey();
                    break;
                case Enums.MainMenuOptions.View_Coding_Calendar:
                    CodeSessionService.GetCodeSessionDates();
                    AnsiConsole.MarkupLine("[red italic]Press Any Key to Return to the Main Menu[/]");
                    Console.ReadKey();
                    break;
                case Enums.MainMenuOptions.Project_Modification:
                    var projectMenuSelection = ProjectMenu.ProjectMenuUserInteraction();
                    ProjectController.ProjectRouter(projectMenuSelection);
                    AnsiConsole.MarkupLine("[red italic]Press Any Key to Return to the Main Menu[/]");
                    Console.ReadKey();
                    break;
                case Enums.MainMenuOptions.Close_Application:
                    running = false;
                    break;
            }
        }
    }
}
