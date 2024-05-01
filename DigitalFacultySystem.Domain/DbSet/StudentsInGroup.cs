using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class StudentsInGroup : BaseEntity
{
    public Guid? StudentId { get; set; }

    public Guid? GroupId { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Student? Student { get; set; }
}
