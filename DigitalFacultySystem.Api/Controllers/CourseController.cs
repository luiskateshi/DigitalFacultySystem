using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class CourseController : BaseController
    {
        public CourseController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var courses = await _unitOfWork.Courses.All();
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var course = await _unitOfWork.Courses.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost("fromStudyPlanSubject")] //the course will be generated from study plan subject
        public async Task<IActionResult> Add([FromBody] StudyPlanSubjectDto studyPlanSubjectDto)
        {
            //map
            var studyPlanSubject = _mapper.Map<StudyPlanSubject>(studyPlanSubjectDto);
            var result = await _unitOfWork.Courses.GenerateCourseFromStudyPlanSubject(studyPlanSubject);
            if (result == null)
            {
                //return bad request if the course already exists
                return BadRequest();
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
            
        }
        [HttpPost("generateStudentsInCourses")]
        public async Task<IActionResult> GenerateStudentsInCourses()
        {
            var result = await _unitOfWork.Courses.GenerateStudentsInCourse();
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var result = await _unitOfWork.Courses.Update(course);
            if (!result)
            {
                return NotFound();
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWork.Courses.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpGet("GetStudentsInCourse")]
        public async Task<IActionResult> GetStudentsInCourse(Guid Id)
        {
            IEnumerable<CourseAttendanceDto> courseAttendanceDtos = await _unitOfWork.Courses.GetStudentsInCourse(Id);
            return Ok(courseAttendanceDtos);
        }

        //update course attendance for a course using a lsit of courseAttendanceDtos
        [HttpPost("UpdateCourseAttendance")]
        public async Task<IActionResult> UpdateCourseAttendance([FromBody] IEnumerable<CourseAttendanceDto> entities)
        {
            var result = await _unitOfWork.Courses.UpdateCourseAttendance(entities);
            if (!result)
            {
                return NotFound();
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        //execute stored procedure to calculate courseAttendance for a course
        [HttpPost("CalculateCourseAttendance")]
        public async Task<IActionResult> CalculateCourseAttendance(Guid Id)
        {
            Guid courseId = Id;
            var result = await _unitOfWork.Courses.CalculateCourseAttendance(courseId);
            if (!result)
            {
                return NotFound();
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

    }
}
