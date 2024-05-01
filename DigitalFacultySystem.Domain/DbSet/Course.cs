using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;
public class Course : BaseEntity
{
    public string? Name { get; set; }

    public Guid? MainLecturerId { get; set; }

    public Guid? SecondLecturerId { get; set; }

    public Guid? LabLecturerId { get; set; }

    public Guid? StudyPlanSubjectId { get; set; }

    public virtual ICollection<ExamRetakeRequest> ExamRetakeRequests { get; set; } = new List<ExamRetakeRequest>();

    public virtual ICollection<StudentInCourse> StudentInCourses { get; set; } = new List<StudentInCourse>();

    public virtual StudyPlanSubject? StudyPlanSubject { get; set; }
}
