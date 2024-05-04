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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _unitOfWork.Courses.Add(course);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(Get), new { id = course.Id }, _mapper.Map<CourseDto>(course));
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
    }
}
