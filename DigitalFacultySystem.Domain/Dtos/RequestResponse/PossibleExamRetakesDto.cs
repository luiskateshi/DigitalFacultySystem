using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class PossibleExamRetakesDto
    {
        public Guid ExamId { get; set; }
        public string ExamName { get; set; }
        public Guid AcademicYearId { get; set; }


        /*
         * This is a list exams that a student can apply to retake.
         
            select * from exams e
            join ExamsSessions es on e.ExamSessionId = es.id
            join AcademicYears a on a.id = es.AcademicYearId
            join StudentsInExams sie on sie.ExamId = e.id
            join Students s on s.id = sie.StudentId
            where s.id = @studentId
            AND a.isActive = 1
            AND NOT EXISTS (
                        SELECT 1
                        FROM ExamRetakeRequests err
                        WHERE err.StudentId = @studentId
                        AND err.planSubjectId = e.PlanSubjectId
                    );



         */
    }
}
