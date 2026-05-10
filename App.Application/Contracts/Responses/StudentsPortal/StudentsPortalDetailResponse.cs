namespace App.Application.Contracts.Responses.StudentsPortal;

public record StudentsPortalDetailResponse
(
    SubjectInfoResponse Subject,
    DepartmentUserInfoResponse Instructor,
    PeriodInfoResponse Period
);

public record SubjectInfoResponse
(
    int Id,
    string Name
);

public record DepartmentUserInfoResponse
(
    int Id,
    string Name
);

public record PeriodInfoResponse
(
    int Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate
);

