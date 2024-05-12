using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class StudentExamGradesDto
    {
        public String SubjectName { get; set; }
        public int CreditsNumber { get; set; }
        public int Grade { get; set; }
        public int Semester { get; set; }
    }


    /*
     *   Query Example to fill in this DTO:
     *   
     *   
        select sb.Name, sps.CreditsNo, MAX(sie.ExamGrade) as 'Exam Grade', sps.Semester from StudentInCoursees sic
        join courses c on sic.CourseId = C.id
        join Students s on s.id = sic.StudentId
        join StudentsInExams sie on sie.StudentId = s.Id
        join StudyPlanSubjects sps on sps.id = c.StudyPlanSubjectId
        join Subjects sb on sb.id = sps.SubjectId
        where s.id = 'D01B4F5C-9867-4DF0-81C6-1A6FE77FDABE'
        and sie.examGrade > 4
        group by sb.Name, sps.CreditsNo, sps.Semester
    */
}
