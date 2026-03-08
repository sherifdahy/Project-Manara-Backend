
namespace App.Core.Entities.Universities;

public class SubjectPrerequisite
{
    // The Subject That Need the Prerequisite
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = default!;

    // The Prerequisite itself
    public int PrerequisiteId { get; set; }
    public Subject Prerequisite { get; set; } = default!;
}
