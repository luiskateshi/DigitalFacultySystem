using AutoMapper;
using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalFacultySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            var student = await _unitOfWork.Students.GetById(id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            var result = _mapper.Map<StudentDto>(student);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _unitOfWork.Students.All();
            var result = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto createStudent)
        {
            var student = _mapper.Map<Student>(createStudent);
            await _unitOfWork.Students.Add(student);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _unitOfWork.Students.Delete(id);
            if (!result)
            {
                return NotFound("Student not found");
            }

            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentDto student)
        {
            var studentToUpdate = _mapper.Map<Student>(student);
            var result = await _unitOfWork.Students.Update(studentToUpdate);
            if (!result)
            {
                return NotFound("Student not found");
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }


    }
}
