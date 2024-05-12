using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class ExamDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ExamSessionId { get; set; }

        public DateOnly? Date { get; set; }

        public Guid? PlanSubjectId { get; set; }

        public int? NumberOfStudents { get; set; }
    }
}
