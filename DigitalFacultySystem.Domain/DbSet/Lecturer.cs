using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;
public class Lecturer : BaseEntity
{
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? IdCard { get; set; }

    public string? Email { get; set; }

    public string? Tel { get; set; }

    public string? Grade { get; set; }

    public Guid? UserId { get; set; }

    public virtual User? User { get; set; }
}
