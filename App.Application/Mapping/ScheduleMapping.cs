using App.Application.Contracts.Responses.Programs;
using App.Core.Entities.Academic;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Mapping;

public class ScheduleMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LectureSchedule, LectureScheduleItemResponse>().Map(dist=>dist.DoctorName,src=>src.Doctor.User.Name);
        config.NewConfig<SectionSchedule, SectionScheduleItemResponse>().Map(dist=>dist.InstructorName,src=>src.Instructor.User.Name);
    }
}
