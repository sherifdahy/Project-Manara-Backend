namespace App.Application.Contracts.Responses.StudentsPortal;

public record StudentPortalDetailResponse
(
    int LectureSchedulesId,
    int RemainingSlots,
    bool IsCurrentEnrolled,
    SubjectResponse Subject,
    DepartmentUserResponse Doctor,
    PeriodResponse Period,
    DayResponse Day
);

public record SubjectResponse
(
    int Id,
    string Name
);

public record DepartmentUserResponse
(
    int Id,
    string Name
);

public record PeriodResponse
(
    int Id,
    TimeOnly StartTime,
    TimeOnly EndTime
);

public record DayResponse
(
    int Id,
    string Value
);
