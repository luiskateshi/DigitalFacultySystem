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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Student>> All()
        {
            try
            {
                return await _dbSet.Where(x=> x.isActive == true)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Firstname)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function eroor", typeof(StudentRepository));
                throw;
            }
        }

        //Get student by User id
        public async Task<Student> GetStudentByUserId(Guid userId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.ApplicationUserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetStudentByUserId function eroor", typeof(StudentRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    return false;
                }
                result.isActive = false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function eroor", typeof(StudentRepository));
                return false;
            }
        }

        public override async Task<bool> Update(Student student)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == student.Id);
                if (result == null)
                {
                    return false;
                }
                MyFieldsMapper.MapFields(student, result);
                result.isActive = true;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(StudentRepository));
                return false;
            }
        }

        //get all courses and grades for a student using this query: 
        public async Task<IEnumerable<StudentExamGradesDto>> GetStudentExamGrades(Guid studentId)
        {
            try
            {
                var query = from sie in _context.StudentsInExams
                            join e in _context.Exams on sie.ExamId equals e.Id
                            join s in _context.Students on sie.StudentId equals s.Id
                            join sps in _context.StudyPlanSubjects on e.PlanSubjectId equals sps.Id
                            join sub in _context.Subjects on sps.SubjectId equals sub.Id
                            where s.Id == studentId && sie.ExamGrade > 4
                            group new { sub.Name, sie.ExamGrade, sps.CreditsNo, sps.Semester } by new { sub.Name, sps.CreditsNo, sps.Semester } into g
                            select new StudentExamGradesDto
                            {
                                SubjectName = g.Key.Name,
                                CreditsNumber = g.Key.CreditsNo,
                                Grade = g.Max(x => x.ExamGrade),
                                Semester = g.Key.Semester
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Error in GetStudentExamGrades function", typeof(StudentRepository));
                throw;
            }
        }







    }
}
