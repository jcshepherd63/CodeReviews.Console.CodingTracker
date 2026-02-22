namespace CodingTimeModel;

public class CodingTime
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration { get; set; }
    public int ProjectId { get; set; }

}