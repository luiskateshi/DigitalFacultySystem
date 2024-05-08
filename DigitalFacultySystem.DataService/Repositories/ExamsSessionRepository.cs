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
    public class ExamsSessionRepository : GenericRepository<ExamsSession>, IExamsSessionRepository
    {
        public ExamsSessionRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        //get all 
        public override async Task<IEnumerable<ExamsSession>> All()
        {
            var result = await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
            return result;
        }

        //update
        public override async Task<bool> Update(ExamsSession entity)
        {
            var examsSession = await _dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (examsSession == null)
            {
                return false;
            }
            MyFieldsMapper.MapFields(entity, examsSession);

            _dbSet.Update(examsSession);
            return true;
        }

        //generate an exam for all courses that are active 




    }


}
