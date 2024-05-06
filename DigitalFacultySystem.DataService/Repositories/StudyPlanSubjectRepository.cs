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
    public class StudyPlanSubjectRepository : GenericRepository<StudyPlanSubject>, IStudyPlanSubjectRepository
    {
        public StudyPlanSubjectRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<StudyPlanSubject>> GetSubjectsByStudyPlan(Guid studyPlanId)
        {
            var subjects = await _context.StudyPlanSubjects
                .Where(sps => sps.StudyPlanId == studyPlanId)
                .ToListAsync();

            return subjects;
        }

        //get all subjects that are not in a specific study plan
        public async Task<IEnumerable<Subject>> GetSubjectsNotInStudyPlan(Guid studyPlanId)
        {
            var subjectsNotInStudyPlan = await _context.Subjects
                .Where(s => !_context.StudyPlanSubjects
                                   .Where(sps => sps.StudyPlanId == studyPlanId)
                                                      .Select(sps => sps.SubjectId)
                                                                         .Contains(s.Id))
                .ToListAsync();

            return subjectsNotInStudyPlan;
        }

        //add a new study plan subject and the name should be code of study plan + name of subject
        public override async Task<bool> Add(StudyPlanSubject entity)
        {
            try
            {
                var studyPlan = await _context.StudyPlans.FirstOrDefaultAsync(x => x.Id == entity.StudyPlanId);
                var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == entity.SubjectId);
                if (studyPlan == null || subject == null)
                {
                    return false;
                }
                entity.Name = studyPlan.Code + " - " + subject.Name;
                await _dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add function eroor", typeof(StudyPlanSubjectRepository));
                return false;
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
                _dbSet.Remove(result);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function eroor", typeof(StudyPlanSubjectRepository));
                return false;
            }
        }

        //update study plan subject
        public override async Task<bool> Update(StudyPlanSubject entity)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                MyFieldsMapper.MapFields(entity, result);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function eroor", typeof(StudyPlanSubjectRepository));
                return false;
            }
        }


    }
}
