using Spectre.Console;
using System;
using System.Collections.Generic;

public class CodeSessionController
{
    public static Calendar CodingCalendarOutput()
    {
        var today = DateTime.Now;
        var calendar = new Calendar(today)
            .Border(TableBorder.Rounded)
            .HeaderStyle(new Style(foreground: Color.Blue, decoration: Decoration.Italic));

        return calendar;
    }


    private static Table CodeSessionTableOutput()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.DarkCyan)
            .Title("[yellow bold]Time Spent Coding[/]");

        table.AddColumn("[italic]Session ID[/]");
        table.AddColumn("[italic]Session Date[/]");
        table.AddColumn("[italic]Session Start Time[/]");
        table.AddColumn("[italic]Session End Time[/]");
        table.AddColumn("[italic]Project Worked On[/]");

        return table;
    }

    public static void OutputSessionListToTable()
    {
        List<CodingTime> codingSessions = CodeSessionService.GetAllCodeSessions();
        var table = CodeSessionTableOutput();

        foreach (CodingTime session in codingSessions)
        {
            table.AddRow(
                session.Id.ToString(),
                session.Date.ToString("MM-dd-yyyy"),
                session.StartTime.ToString(),
                session.EndTime.ToString(),
                ProjectService.GetProjectNameFromId(session));
        }
        AnsiConsole.Write(table);
    }
}

