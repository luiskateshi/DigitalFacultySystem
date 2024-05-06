using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class ExamsSession : BaseEntity
{
    public string? Name { get; set; }   
    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public Guid? AcademicYearId { get; set; }

    public virtual AcademicYear? AcademicYear { get; set; }

    public virtual ICollection<ExamRetakeRequest> ExamRetakeRequests { get; set; } = new List<ExamRetakeRequest>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
