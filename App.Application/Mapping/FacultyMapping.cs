using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Mapping;

public class FacultyMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //TODO (Make Any Thing About This )

        //config.NewConfig<Faculty, FacultyResponse>()
        //     .Map(dest => dest.NumberOfProgramUsers,
        //        src => src.Departments
        //     .SelectMany(d => d.Programs)
        //     .SelectMany(p => p.ProgramUsers)
        //     .Count());
    }
}
