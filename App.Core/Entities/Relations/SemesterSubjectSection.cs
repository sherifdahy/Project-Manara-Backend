namespace MappingOfManaraaProject.Entities.Relations
{
    public class SemesterSubjectSection
    {
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public int SectionId { get; set; }

        public Semester Semester { get; set; } = default!;      
        public Subject Subject { get; set; } = default!;
        public Section Section { get; set; } = default!;
    }
}
