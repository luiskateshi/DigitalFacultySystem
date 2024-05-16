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
            try
            {
                var result = await _unitOfWork.ExamRetakeRequests.AddRequest(requestDto);

                await _unitOfWork.CompleteAsync();
                if (result)
                {
                    return Ok();
                }

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetRequestsByStudent")]
        public async Task<IActionResult> GetRequestsByStudent(Guid Id)
        {
            var requestDtos = await _unitOfWork.ExamRetakeRequests.GetRequestsByStudent(Id);
            return Ok(requestDtos);
        }

        [HttpGet("GetPossibleExamRetakes")]
        public async Task<IActionResult> GetPossibleExamRetakes(Guid Id)
        {
            var possibleExamRetakes = await _unitOfWork.ExamRetakeRequests.GetPossibleExamRetakes(Id);
            return Ok(possibleExamRetakes);
        }
    }
}
