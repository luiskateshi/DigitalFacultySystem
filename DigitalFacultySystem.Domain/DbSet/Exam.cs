using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;
public class Exam : BaseEntity
{
    public string Name { get; set; }

    public Guid? ExamSessionId { get; set; }

    public DateOnly? Date { get; set; }

    public Guid? PlanSubjectId { get; set; }

    public virtual ExamsSession? ExamSession { get; set; }

    public virtual StudyPlanSubject? PlanSubject { get; set; }

    public virtual ICollection<StudentsInExam> StudentsInExams { get; set; } = new List<StudentsInExam>();
}
