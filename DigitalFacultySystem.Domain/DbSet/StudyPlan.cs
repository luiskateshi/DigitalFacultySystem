using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class StudyPlan : BaseEntity
{
    public string? Name { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public Guid? DegreeProgId { get; set; }

    public String? Code { get; set; }

    public virtual DegreeProgram? DegreeProg { get; set; }

    public virtual ICollection<Generation> Generations { get; set; } = new List<Generation>();

    public virtual ICollection<StudyPlanSubject> StudyPlanSubjects { get; set; } = new List<StudyPlanSubject>();
}
