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
    public class GenerationRepository : GenericRepository<Generation>, IGenerationRepository
    {
        public GenerationRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Generation>> All()
        {
            var result =  await _dbSet.Where(x => x.isActive == true)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .ToListAsync();
            return result;
        }


        //update
        public override async Task<bool> Update(Generation entity)
        {
            var generation = await _dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (generation == null)
            {
                return false;
            }
            MyFieldsMapper.MapFields(entity, generation);

            _dbSet.Update(generation);
            return true;
        }

    }
}
