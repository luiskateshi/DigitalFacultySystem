using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class CourseAttendanceDto
    {
        public Guid Id { get; set; }
        public int? SeminarHoursAttended { get; set; }
        public int? LabHoursAttended { get; set; }
        public Guid? StudentInCourseId { get; set; }
        public StudentDto student { get; set; }
        public CourseDto course { get; set; }

    }
}
