using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;
public class StudentInCourse : BaseEntity
{
    public Guid? StudentId { get; set; }

    public Guid? AcademicYearId { get; set; }

    public Guid? CourseId { get; set; }

    public virtual AcademicYear? AcademicYear { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<CourseAttendance> CourseAttendances { get; set; } = new List<CourseAttendance>();

    public virtual Student? Student { get; set; }
}
