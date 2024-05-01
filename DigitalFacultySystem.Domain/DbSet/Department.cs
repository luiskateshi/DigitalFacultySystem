using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;
public class Department : BaseEntity
{
    public string? Name { get; set; }

    public virtual ICollection<DegreeProgram> DegreePrograms { get; set; } = new List<DegreeProgram>();
}
