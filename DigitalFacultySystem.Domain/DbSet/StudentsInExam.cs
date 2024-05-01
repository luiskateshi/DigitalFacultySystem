using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;
public class StudentsInExam : BaseEntity
{
    public Guid? StudentId { get; set; }

    public Guid? ExamId { get; set; }

    public short? Attended { get; set; }

    public int? ExamGrade { get; set; }

    public virtual Exam? Exam { get; set; }

    public virtual Student? Student { get; set; }
}
