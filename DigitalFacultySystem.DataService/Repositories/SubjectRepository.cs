using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        //get all active subjects  from _dbSet
        public override async Task<IEnumerable<Subject>> All()
        {
            return await _dbSet.Where(x => x.isActive == true)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Name)
                    .ToListAsync();
        }


        //get all subjects for a specific studyplan from _dbSet.StudyPlanSubjects order by semester in studyplansubject
        public async Task<IEnumerable<Subject>> AllForStudyPlan(Guid studyPlanId)
        {
            return await _dbSet.Where(x => x.StudyPlanSubjects.Any(y => y.StudyPlanId == studyPlanId))
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.StudyPlanSubjects.FirstOrDefault(y => y.StudyPlanId == studyPlanId).Semester)
                    .ToListAsync();
        }

        //update subject
        public override async Task<bool> Update(Subject entity)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
            {
                return false;
            }
            MyFieldsMapper.MapFields(entity, result);
            result.isActive = true;
            return true;
        }

        
    }
}
