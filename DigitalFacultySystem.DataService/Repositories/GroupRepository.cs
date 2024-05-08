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
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DigitalFacultySystem.DataService.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        
        public override async Task<IEnumerable<Group>> All()
        {
            var result = await _dbSet                
                .Where(x => x.isActive)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
            return result;
        }
        //get group by id but also fill the generation field 
        public override async Task<Group> GetById(Guid id)
        {
            var result = await _dbSet
                .Include(x => x.Generation)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }


        public override async Task<bool> Update(Group entity)
        {
            
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
