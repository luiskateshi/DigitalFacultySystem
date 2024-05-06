using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class StudentInCourseDto
    {
        //these data will be filled in by the system using a stored procedure
        public Guid Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? AcademicYearId { get; set; }
        public Guid? CourseId { get; set; }

    }
}
