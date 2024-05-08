using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class Generation : BaseEntity
{
    public string Name { get; set; }

    public Guid? StudyPlanId { get; set; }

    public Guid? StartAcademicYearId { get; set; }

    public int? CurrentSemesterOfStudies { get; set; }

    public Guid? DegreeId { get; set; }

    public virtual DegreeProgram? Degree { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual AcademicYear? StartAcademicYear { get; set; }

    public virtual StudyPlan? StudyPlan { get; set; }
}
