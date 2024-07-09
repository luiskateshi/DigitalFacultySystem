using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        //get Exam by id
        public override async Task<Exam> GetById(Guid id)
        {
           var result = await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<ExamDto>> GetExamsByExamSession(Guid examSessionId)
        {
            var result = await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.ExamSessionId == examSessionId)
                .Select(x => new ExamDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    ExamSessionId = x.ExamSessionId,
                    PlanSubjectId = x.PlanSubjectId,
                    NumberOfStudents = _context.StudentsInExams.Count(se => se.ExamId == x.Id)
                })
                .ToListAsync();

            return result;
        }
       

        //get students in exam by exam id
        public async Task<IEnumerable<StudentsInExamDto>> GetStudentsInExam(Guid examId)
        {
            var result = await _context.StudentsInExams
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.ExamId == examId)
                .Include(x => x.Student)
                .Select(x => new StudentsInExamDto
                {
                    StudentId = x.StudentId,
                    ExamId = x.ExamId,
                    Attended = x.Attended,
                    ExamGrade = x.ExamGrade,
                    StudentFirstName = x.Student.Firstname,
                    StudentLastName = x.Student.Lastname,
                    IsActive = x.isActive
                })
                .OrderBy(x => x.StudentFirstName)
                .ToListAsync();

            return result;
        }

        //update exam
        public override async Task<bool> Update(Exam entity)
        {
            var exam = await _dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (exam == null)
            {
                return false;
            }
            exam.Name = entity.Name;
            exam.Date = entity.Date;

            _dbSet.Update(exam);
            return true;
        }

        //update a list of students in exam
        public async Task<bool> UpdateStudentsInExam(IEnumerable<StudentsInExamDto> studentsInExam)
        {
            foreach (var studentInExam in studentsInExam)
            {
                var student = await _context.StudentsInExams
                    .Where(x => x.StudentId == studentInExam.StudentId && x.ExamId == studentInExam.ExamId)
                    .FirstOrDefaultAsync();

                if (student == null)
                {
                    return false;
                }
                student.Attended = studentInExam.Attended;
                student.ExamGrade = studentInExam.ExamGrade;

                _context.StudentsInExams.Update(student);
            }

            return true;
        }



    }
}
