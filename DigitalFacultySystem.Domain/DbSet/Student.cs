using DigitalFacultySystem.Entities.DbSet;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalFacultySystem.Domain.Entities;

public class Student : BaseEntity
{
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? Birthdate { get; set; } = new DateOnly(2000, 1, 1);

    public string? IdCard { get; set; }

    public string? Email { get; set; }

    public string? Tel { get; set; }

    //add a field for current semester of studies and set default value to 1
    public int? CurrentSemester { get; set; } = 1;

    public Guid? ApplicationUserId { get; set; }

    //aspiringDegree field 
    public Guid? DegreeProgramId { get; set; }

    public List<ExamRetakeRequest> ExamRetakeRequests { get; set; } = new List<ExamRetakeRequest>();

    public List<StudentInCourse> StudentInCourses { get; set; } = new List<StudentInCourse>();

    public List<StudentsInExam> StudentsInExams { get; set; } = new List<StudentsInExam>();

    public StudentsInGroup? StudentsInGroup { get; set; } // Assuming each student belongs to only one group

    public virtual ApplicationUser? User { get; set; }

    public virtual DegreeProgram? DegreeProgram { get; set; }
}
