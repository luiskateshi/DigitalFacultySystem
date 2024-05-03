using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public string? Syllabus { get; set; }
    }
}
