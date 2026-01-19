namespace App.Core.Entities.Academic;
public class ExamSeating
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ExamRoom { get; set; } = string.Empty;
    public string SeatNumber { get; set; } = string.Empty;
    public DateTime ExamDateTime { get; set; }
}
