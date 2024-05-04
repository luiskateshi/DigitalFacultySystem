using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class GenerationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? StudyPlanId { get; set; }
        public Guid? StartAcademicYearId { get; set; }
        public int? CurrentYearOfStudies { get; set; }
        public Guid? DegreeId { get; set; }
    }
}
