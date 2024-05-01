using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class Subject : BaseEntity
{
    public string? Name { get; set; }

    public string? Notes { get; set; }

    public string? Syllabus { get; set; }

    public virtual ICollection<StudyPlanSubject> StudyPlanSubjects { get; set; } = new List<StudyPlanSubject>();
}
