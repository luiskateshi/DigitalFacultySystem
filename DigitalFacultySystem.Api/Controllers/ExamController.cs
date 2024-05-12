using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class ExamController : BaseController
    {
        public ExamController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        //get Exam by id
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var exam = await _unitOfWork.Exams.GetById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ExamDto>(exam));
        }

        //get all exams by exam session id
        [HttpGet("GetExamsByExamSession")]
        public async Task<IActionResult> GetExamsByExamSession(Guid Id)
        {
            var examSessionId = Id;
            var exams = await _unitOfWork.Exams.GetExamsByExamSession(examSessionId);
            return Ok(exams);

        }

        //get students in exam by exam id
        [HttpGet("GetStudentsInExam")]
        public async Task<IActionResult> GetStudentsInExam(Guid Id)
        {
            var examId = Id;
            var studentsInExam = await _unitOfWork.Exams.GetStudentsInExam(examId);
            return Ok(studentsInExam);
        }

        //update exam
        [HttpPut]
        public async Task<IActionResult> UpdateExam(ExamDto examDto)
        {
            var exam = _mapper.Map<Exam>(examDto);
            var result = await _unitOfWork.Exams.Update(exam);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        //update students in exam
        [HttpPost("UpdateStudentsInExam")]
        public async Task<IActionResult> UpdateStudentsInExam([FromBody]  IEnumerable<StudentsInExamDto> studentsInExamDtos)
        {
            var result = await _unitOfWork.Exams.UpdateStudentsInExam(studentsInExamDtos);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
}
