using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class Group : BaseEntity
{
    public Guid? GenerationId { get; set; }

    public string? Name { get; set; }

    public virtual Generation? Generation { get; set; }

    public virtual ICollection<StudentsInGroup> StudentsInGroups { get; set; } = new List<StudentsInGroup>();
}
