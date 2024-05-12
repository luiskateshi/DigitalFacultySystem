using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
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
    public class ExamRetakeRequestRepository : GenericRepository<ExamRetakeRequest>, IExamRetakeRequestRepository
    {
        public ExamRetakeRequestRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        //add a new request
        public async Task<bool> AddRequest(ExamRetakeRequestDto request)
        {
            try
            {
                var newRequest = new ExamRetakeRequest
                {
                    Id = Guid.NewGuid(),
                    StudentId = request.StudentId,
                    PlanSubjectId = request.PlanSubjectId,
                    DateOfRequest = DateOnly.FromDateTime(DateTime.Today),
                    isActive = true,
                    ExamSessionId = request.ExamSessionId
                };

                await _dbSet.AddAsync(newRequest);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding new request");
                return false;
            }
        }

        //get all requests that are active and that a student has made
        public async Task<IEnumerable<ExamRetakeRequestDto>> GetRequestsByStudent(Guid studentId)
        {
            var result = await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.StudentId == studentId)
                .Select(x => new ExamRetakeRequestDto
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    PlanSubjectId = x.PlanSubjectId,
                    DateOfRequest = x.DateOfRequest,
                    ExamSessionId = x.ExamSessionId
                })
                .ToListAsync();

            return result;
        }

        
        public async Task<IEnumerable<PossibleExamRetakesDto>> GetPossibleExamRetakes(Guid studentId)
        {
            var result = (from e in _context.Exams
                          join es in _context.ExamsSessions on e.ExamSessionId equals es.Id
                          join a in _context.AcademicYears on es.AcademicYearId equals a.Id
                          join sie in _context.StudentsInExams on e.Id equals sie.ExamId
                          join s in _context.Students on sie.StudentId equals s.Id
                          where s.Id == studentId && a.isActive == true
                          && !_context.ExamRetakeRequests.Any(err =>
                                    err.StudentId == studentId && err.PlanSubjectId == e.PlanSubjectId)
                          select new PossibleExamRetakesDto
                          {
                              ExamId = e.Id,
                              ExamName = e.Name
                          }).ToList();

            return result;
        }

    }
}
