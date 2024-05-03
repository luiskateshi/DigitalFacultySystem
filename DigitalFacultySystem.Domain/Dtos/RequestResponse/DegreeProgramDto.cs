using DigitalFacultySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class DegreeProgramDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Grade { get; set; }
        public int? StudyLength { get; set; }
        public Guid? DepartmentId { get; set; }

    }
}
