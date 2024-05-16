using AutoMapper;
using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.DbSet;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Entities.Dtos.SecurityDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalFacultySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IUserAccount _userAccount;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper, IUserAccount userAccount) : base(unitOfWork, mapper)
        {
            _userAccount = userAccount;
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

            //firstly create a user account for this student
            var user = new UserDto
            {
                Name = createStudent.Firstname + " " + createStudent.Lastname,
                Email = createStudent.Email,
                Role = "student",
                Password = "Student@123" //default password for student
            };
            var userResult = await _userAccount.CreateAccount(user);
            if (!userResult.Flag)
            {
                return BadRequest(userResult.Message);
            }
            createStudent.ApplicationUserId = Guid.Parse(userResult.Message);
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

        [HttpGet("GetStudentByUserId")]
        public async Task<IActionResult> GetStudentByUserId(Guid Id)
        {
            var student = await _unitOfWork.Students.GetStudentByUserId(Id);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            var result = student.Id;
            return Ok(result);
        }

        [HttpGet("GetAllStudentGrades")]
        public async Task<IActionResult> GetAllStudentGrades(Guid Id)
        {
            var student = await _unitOfWork.Students.GetById(Id);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var grades = await _unitOfWork.Students.GetStudentExamGrades(Id);
            return Ok(grades);
        }

        [HttpGet("GetAllMyGrades")]
        public async Task<IActionResult> GetAllMytGrades(Guid Id)
        {
            var student = await _unitOfWork.Students.GetStudentByUserId(Id);
            var grades = await _unitOfWork.Students.GetStudentExamGrades(student.Id);
            return Ok(grades);
        }




    }
}
