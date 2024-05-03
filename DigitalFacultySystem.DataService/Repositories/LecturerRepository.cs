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
    public class LecturerRepository : GenericRepository<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        //all
        public override async Task<IEnumerable<Lecturer>> All()
        {
            return await _dbSet.Where(x => x.isActive == true)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Firstname)
                    .ToListAsync();
        }

        //update 
        public override async Task<bool> Update(Lecturer entity)
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
