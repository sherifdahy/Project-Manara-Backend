using App.Application.Contracts.Requests.LectureSchedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.ProgramSchedules;

public class SaveLectureSchedulesRequestValidator : AbstractValidator<SaveLectureSchedulesRequest>
{
    public SaveLectureSchedulesRequestValidator()
    {
        RuleFor(x => x.Schedules)
         .NotNull();

        RuleForEach(x => x.Schedules).SetValidator(new LectureScheduleItemRequestValidator());

        RuleFor(x => x.Schedules)
            .Must(x => x.Select(a => (a.PeriodId, a.SubjectId, a.DayId)).Distinct().Count() == x.Count)
            .When(x=>x.Schedules is not null && x.Schedules.Count > 0)
            .WithMessage(x=> BuildDuplicateMessage(x.Schedules));

    }

    private static string BuildDuplicateMessage(List<LectureScheduleItemRequest> Schedules)
    {
        if (Schedules is null || Schedules.Count == 0)
            return "Schedules list is empty.";

        var duplicates = Schedules
            .GroupBy(a => new { a.SubjectId, a.PeriodId, a.DayId })
            .Where(g => g.Count() > 1)
            .Select(g =>
                $"(SubjectId: {g.Key.SubjectId}, PeriodId: {g.Key.PeriodId}, DayId: {g.Key.DayId})")
            .ToList();

        return duplicates.Count > 0
            ? $"Duplicate Schedules detected: {string.Join(", ", duplicates)}. " +
              "The same subject cannot be assigned to the same period and day more than once."
            : "Validation failed.";
    }
}
