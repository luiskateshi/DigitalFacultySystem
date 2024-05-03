using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<DegreeProgramDto> DegreePrograms { get; set; } = new List<DegreeProgramDto>();
    }
}
