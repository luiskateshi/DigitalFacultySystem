using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? MainLecturerId { get; set; }
        public Guid? SecondLecturerId { get; set; }
        public Guid? LabLecturerId { get; set; }
        public Guid? StudyPlanSubjectId { get; set; }
    }
}
