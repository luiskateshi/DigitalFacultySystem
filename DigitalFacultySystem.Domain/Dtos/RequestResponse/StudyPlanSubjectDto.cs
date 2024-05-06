using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class StudyPlanSubjectDto
    {
        public Guid Id { get; set; }
        public Guid? SubjectId { get; set; }

        public Guid? StudyPlanId { get; set; }

        public String? Name { get; set; }

        public int? CreditsNo { get; set; }

        public int? TotalHoursLectures { get; set; }

        public int? TotalHoursSeminars { get; set; }

        public int? TotalHoursLab { get; set; }

        public int? Semester { get; set; }       

        public SubjectDto? Subject { get; set; }
    }
}
