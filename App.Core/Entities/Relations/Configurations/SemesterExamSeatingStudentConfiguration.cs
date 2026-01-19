using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SemesterExamSeatingStudentConfiguration : IEntityTypeConfiguration<SemesterExamSeatingStudent>
{
    public void Configure(EntityTypeBuilder<SemesterExamSeatingStudent> builder)
    {
        builder.HasKey(x =>new {x.SemesterId,x.ExamSeatingId,x.StudentId});
    }
}
