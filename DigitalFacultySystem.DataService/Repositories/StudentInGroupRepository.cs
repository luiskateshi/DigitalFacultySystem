using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService.Repositories
{
    public class StudentInGroupRepository : GenericRepository<StudentsInGroup>, IStudentInGroupRepository
    {
        public StudentInGroupRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        //delete override delete permanently
        public override async Task<bool> Delete(Guid id)
        {//delete by studentid
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.StudentId == id);
                if (result == null)
                {
                    return false;
                }
                _dbSet.Remove(result);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function eroor", typeof(StudentInGroupRepository));
                return false;
            }
           
        }

        public async Task<IEnumerable<Student>> GetStudentsByGroup(Guid groupId)
        {
            var students = await _context.StudentsInGroups
                .Where(sg => sg.GroupId == groupId && sg.isActive)
                .Select(sg => sg.Student)
                .ToListAsync();

            return students;
        }


        //get all students that are interested in a specific degree program and that do not have an active record in the studentInGroup table
        public async Task<IEnumerable<Student>> GetStudentsWithNoGroup(Guid degreeId)
        {
            var studentsWithoutActiveGroup = await _context.Students
                .Where(s => s.DegreeProgramId == degreeId &&
                            !_context.StudentsInGroups
                                .Where(sg => sg.isActive)
                                .Select(sg => sg.StudentId)
                                .Contains(s.Id))
                .ToListAsync();

            return studentsWithoutActiveGroup;
        }


    }
}
