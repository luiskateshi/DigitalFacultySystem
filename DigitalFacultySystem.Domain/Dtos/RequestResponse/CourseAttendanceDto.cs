using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class CourseAttendanceDto
    {
        public int? SeminarHoursAttended { get; set; } = 0;
        public int? LabHoursAttended { get; set; } = 0;
        public bool isAttended { get; set; } = false;
        public bool requirementsAreFilled { get; set; } = false;
        public Guid? StudentInCourseId { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentLastName { get; set; }
        public string? groupName { get; set; }

    }
}
