using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class StudyPlanSubject : BaseEntity
{
    public Guid? SubjectId { get; set; }

    public Guid? StudyPlanId { get; set; }

    public int? CreditsNo { get; set; }

    public int? TotalHoursLectures { get; set; }

    public int? TotalHoursSeminars { get; set; }

    public int? TotalHoursLab { get; set; }

    public int? Semester { get; set; }
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual StudyPlan? StudyPlan { get; set; }

    public virtual Subject? Subject { get; set; }
}
