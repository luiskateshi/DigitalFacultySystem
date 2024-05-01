using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;
public class AcademicYear : BaseEntity
{
    public string? Name { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<ExamsSession> ExamsSessions { get; set; } = new List<ExamsSession>();

    public virtual ICollection<Generation> Generations { get; set; } = new List<Generation>();

    public virtual ICollection<StudentInCourse> StudentInCourses { get; set; } = new List<StudentInCourse>();
}
