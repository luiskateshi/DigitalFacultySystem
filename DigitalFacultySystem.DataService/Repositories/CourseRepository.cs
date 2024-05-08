using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Course>> All()
        {
            var result = await _dbSet.Where(x => x.isActive)
                .AsNoTracking().
                AsSplitQuery().
                ToListAsync(); 
            return result;                
        }

        //override the get by id method to include the study plan subject
        public override async Task<Course> GetById(Guid id)
        {
            var result = await _dbSet.Where(x => x.Id == id)
                .Include(x => x.StudyPlanSubject)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }

        //execute stored procedure to generate studentsInCourse
        public async Task<bool> GenerateStudentsInCourse()
        {
            try
            {
                //var parameters = new[] {
                //    new SqlParameter("@courseId", SqlDbType.UniqueIdentifier) { Value = courseId }
                //};

                await _context.Database.ExecuteSqlRawAsync("EXEC SP_InsertStudentsInCourse ");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing stored procedure");
                return false;
            }
        }

        //deactivate course
        public override async Task<bool> Delete(Guid courseId)
        {
            var course = await _dbSet.Where(x => x.Id == courseId)
                .FirstOrDefaultAsync();
            if (course == null)
            {
                return false;
            }
            course.isActive = false;
            _dbSet.Update(course);
            return true;
        }



        //update
        public override async Task<bool> Update(Course entity)
        {
            var course = await _dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (course == null)
            {
                return false;
            }
            MyFieldsMapper.MapFields(entity, course);

            _dbSet.Update(course);
            return true;
        }

        //generate a course from studyplansubject and insert it in the db
        public async Task<Course> GenerateCourseFromStudyPlanSubject(StudyPlanSubject studyPlanSubject)
        {
            //first check if the course already exists a course for this study plan subject
            var course = await _dbSet.Where(x => x.StudyPlanSubjectId == studyPlanSubject.Id)
                .FirstOrDefaultAsync();
            if (course != null)
            {
                if(course.isActive == true)
                {
                    return null;
                }
                if(course.isActive == false)
                {
                    course.isActive = true;
                    return course;
                }
                
            }
            course = new Course
            {
                StudyPlanSubjectId = studyPlanSubject.Id,
                Name = studyPlanSubject.Name,
                isActive = true
            };
            await _dbSet.AddAsync(course);
            return course;
        }

        //write the query below in entity framework:
        /*select s.Firstname, s.Lastname, g.Name, ca.SeminarHoursAttended, ca.LabHoursAttended, ca.isAttended
        select s.Firstname, s.Lastname, g.Name, ca.SeminarHoursAttended, ca.LabHoursAttended, ca.isAttended
          from Students s 
          join StudentsInGroups sig on sig.StudentId = s.id
          join Groups g on g.Id = sig.GroupId
          join StudentInCoursees sc on sc.StudentId = s.id
          join CourseAttendances ca on ca.StudentInCourseId = sc.Id
          where sc.CourseId = courseId
                */
        public async Task<IEnumerable<CourseAttendanceDto>> GetStudentsInCourse(Guid courseId)
        {
            var result = await _context.CourseAttendances
                .Where(x => x.StudentInCourse.CourseId == courseId)
                .Include(x => x.StudentInCourse.Student)
                    .ThenInclude(x => x.StudentsInGroup)
                        .ThenInclude(x => x.Group)
                .Select(x => new CourseAttendanceDto
                {
                    StudentId = x.StudentInCourse.StudentId,
                    StudentName = x.StudentInCourse.Student.Firstname,
                    StudentLastName = x.StudentInCourse.Student.Lastname,
                    groupName = x.StudentInCourse.Student.StudentsInGroup.Group.Name,
                    StudentInCourseId = x.StudentInCourseId,
                    SeminarHoursAttended = x.SeminarHoursAttended,
                    LabHoursAttended = x.LabHoursAttended,
                    isAttended = x.isAttended
                })
                .ToListAsync();
            return result;
        }

        //update course attendance that gets as a parameter a list of course attendance dtos
        public async Task<bool> UpdateCourseAttendance(IEnumerable<CourseAttendanceDto> courseAttendanceDtos)
        {
            foreach (var courseAttendanceDto in courseAttendanceDtos)
            {
                var courseAttendance = await _context.CourseAttendances
                    .Where(x => x.StudentInCourseId == courseAttendanceDto.StudentInCourseId)
                    .FirstOrDefaultAsync();
                if (courseAttendance == null)
                {
                    continue;
                }

                courseAttendance.SeminarHoursAttended = courseAttendanceDto.SeminarHoursAttended;
                courseAttendance.LabHoursAttended = courseAttendanceDto.LabHoursAttended;
                _context.CourseAttendances.Update(courseAttendance);
            }
            return true;
        }

        //call stored procedure to calculate if the course was attended or not 
        public async Task<bool> CalculateCourseAttendance(Guid courseId)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC SP_CalculateCourseAttendance @courseId", new SqlParameter("@courseId", courseId));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing stored procedure");
                return false;
            }
        }


        

    }
}
