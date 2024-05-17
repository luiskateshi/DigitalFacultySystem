using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class studyPlan : BaseEntity
{
    public Guid? StudentId { get; set; }

    public DateOnly? DateOfRequest { get; set; }

    public Guid? ExamId { get; set; }

    public virtual Exam? Exam { get; set; }

    public virtual Student? Student { get; set; }
}
