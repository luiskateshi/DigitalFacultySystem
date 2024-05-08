using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        //get all by exams session id
        public async Task<IEnumerable<Exam>> AllByExamsSessionId(Guid examsSessionId)
        {
            var result = await _dbSet.Where(x => x.ExamSessionId == examsSessionId)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
            return result;
        }
        //get all by degree program id (join tables)

        //select* exams e
        //inner join examSession es on es.id = e.examSessionId
        //inner join studyPlanSubject sps on sps.id = e.planSubjectId
        //inner join studyPlan sp on sp.id = sps.StudyPlanId
        //where e.examSessionId = @examSessionId
        //AND sp.degreeProgId = @degreeId
        //AND e.isActive = true

        public async Task<IEnumerable<Exam>> AllByDegreeProgramId(Guid degreeId, Guid examSessionId)
        {
            var result = await _dbSet
                .Where(x => x.isActive)
                .Where(x => x.ExamSessionId == examSessionId)
                .Include(x => x.ExamSession)
                .Include(x => x.PlanSubject)
                .ThenInclude(x => x.StudyPlan)
                .Where(x => x.PlanSubject.StudyPlan.DegreeProgId == degreeId)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
            return result;
        }


        //check if there are any students that still havent passed a specific study plan subject
        public async Task<bool> Check(Guid planSubjectId)
        {     
            var result = await _dbSet
                .Where(x => x.PlanSubjectId == planSubjectId)
                .Where(x => x.isActive)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            return true;
        }
        

    }
}
