using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class ExamsSessionController : BaseController
    {
        public ExamsSessionController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var examSessions = await _unitOfWork.ExamsSessions.All();
            return Ok(_mapper.Map<IEnumerable<ExamSessionDto>>(examSessions));
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var examSession = await _unitOfWork.ExamsSessions.GetById(id);
            if (examSession == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ExamSessionDto>(examSession));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExamSessionDto examSessionsDto)
        {
            var examSession = _mapper.Map<ExamsSession>(examSessionsDto);
            await _unitOfWork.ExamsSessions.Add(examSession);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ExamSessionDto examSessionsDto)
        {
            var examSession = _mapper.Map<ExamsSession>(examSessionsDto);
            var result = await _unitOfWork.ExamsSessions.Update(examSession);
            if (!result)
            {
                return NotFound();
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }


    }
}
