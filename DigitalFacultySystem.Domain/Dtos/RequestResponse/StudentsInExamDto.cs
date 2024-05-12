using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class StudentsInExamDto
    {
        public Guid? StudentId { get; set; }

        public Guid? ExamId { get; set; }

        public bool? Attended { get; set; }

        public int? ExamGrade { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        public bool? IsActive { get; set; }

    }
}
