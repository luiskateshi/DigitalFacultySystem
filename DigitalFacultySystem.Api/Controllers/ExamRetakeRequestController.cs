using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class ExamRetakeRequestController : BaseController
    {
        public ExamRetakeRequestController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(ExamRetakeRequestDto requestDto)
        {
            var request = _mapper.Map<ExamRetakeRequest>(requestDto);


            var result = await _unitOfWork.ExamRetakeRequests.Add(request);

            if (result)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("GetRequestsByStudent")]
        public async Task<IActionResult> GetRequestsByStudent(Guid studentId)
        {
            var requestDtos = await _unitOfWork.ExamRetakeRequests.GetRequestsByStudent(studentId);
            return Ok(requestDtos);
        }

        [HttpGet("GetPossibleExamRetakes")]
        public async Task<IActionResult> GetPossibleExamRetakes(Guid studentId)
        {
            var possibleExamRetakes = await _unitOfWork.ExamRetakeRequests.GetPossibleExamRetakes(studentId);
            return Ok(possibleExamRetakes);
        }
    }
}
