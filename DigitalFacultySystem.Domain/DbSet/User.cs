using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class User : BaseEntity
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public DateOnly? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Lecturer> Lecturers { get; set; } = new List<Lecturer>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
