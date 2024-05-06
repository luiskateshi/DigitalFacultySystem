using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class StudentInGroupController : BaseController
    {
        public StudentInGroupController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("byGroup")]
        public async Task<IActionResult> GetStudentsByGroup(Guid id)
        {
            var students = await _unitOfWork.StudentsInGroups.GetStudentsByGroup(id);
            var result = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(result);
        }

        [HttpGet("noGroup")]
        public async Task<IActionResult> GetStudentsWithoutGroup(Guid id)
        {
            var students = await _unitOfWork.StudentsInGroups.GetStudentsWithNoGroup(id);
            var result = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentToGroup([FromBody] StudentInGroupDto studentInGroupDto)
        {
            try
            {
                var studentInGroup = _mapper.Map<StudentsInGroup>(studentInGroupDto);
                studentInGroup.isActive = true;
                var a = await _unitOfWork.StudentsInGroups.Add(studentInGroup);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveStudentFromGroup(Guid id)
        {
            var result = await _unitOfWork.StudentsInGroups.Delete(id);
            if (!result)
            {
                return NotFound("Student not found in group");
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}
