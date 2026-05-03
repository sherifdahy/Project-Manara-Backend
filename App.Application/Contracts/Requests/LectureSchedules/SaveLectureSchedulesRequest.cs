
namespace App.Application.Contracts.Requests.LectureSchedules;

public class SaveLectureSchedulesRequest
{
    public List<LectureScheduleItemRequest> Schedules { get; set; } = [];
}