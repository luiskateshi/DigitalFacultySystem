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
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Course>> All()
        {
            var result = await _dbSet.Where(x => x.isActive)
                .AsNoTracking().
                AsSplitQuery().
                ToListAsync(); 
            return result;                
        }

        //update
        public override async Task<bool> Update(Course entity)
        {
            var course = await _dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (course == null)
            {
                return false;
            }
            MyFieldsMapper.MapFields(entity, course);

            _dbSet.Update(course);
            return true;
        }
    }
}
