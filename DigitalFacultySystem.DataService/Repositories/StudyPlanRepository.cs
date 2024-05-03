using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalFacultySystem.DataService.Repositories
{
    public class StudyPlanRepository : GenericRepository<StudyPlan>, IStudyPlanRepository
    {
        public StudyPlanRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<StudyPlan>> All()
        {
            return await _dbSet
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Name)
                    .ToListAsync();
        }
 
        public override async Task<bool> Update(StudyPlan entity)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
            {
                return false;
            }
            MyFieldsMapper.MapFields(entity, result);

            return true;
        }

        //inactivate all study plans for a degree program
        public async Task InactivateAllOtherPlans(Guid? degreeProgramId)
        {
            var studyPlans = await _dbSet
                .Where(x => x.DegreeProgId == degreeProgramId)
                .ToListAsync();

            foreach (var studyPlan in studyPlans)
            {
                studyPlan.isActive = false;
            }
        }

        //override add method
        public override async Task<bool> Add(StudyPlan entity)
        {
            //inactivate all study plans for a degree program
            await InactivateAllOtherPlans(entity.DegreeProgId);
            entity.isActive = true;
            await _dbSet.AddAsync(entity);
            return true;
        }
    }
}
