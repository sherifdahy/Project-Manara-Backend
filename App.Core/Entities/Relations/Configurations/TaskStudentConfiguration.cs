using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations
{
    public class TaskStudentConfiguration : IEntityTypeConfiguration<TaskStudent>
    {
        public void Configure(EntityTypeBuilder<TaskStudent> builder)
        {
        }
    }
}
