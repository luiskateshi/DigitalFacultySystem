using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class DegreeProgram : BaseEntity
{
    public string? Name { get; set; }

    public string? Grade { get; set; }

    public int? StudyLength { get; set; }

    public Guid? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Generation> Generations { get; set; } = new List<Generation>();

    public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
}
