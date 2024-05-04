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
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Group>> All()
        {
            var result =  await _dbSet.Where(x => x.isActive == true)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .ToListAsync();
            return result;
        }


        public override async Task<bool> Update(Group entity)
        {
            var group = await _dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (group == null)
            {
                return false;
            }
            MyFieldsMapper.MapFields(entity, group);

            _dbSet.Update(group);
            return true;
        }
    }
}
