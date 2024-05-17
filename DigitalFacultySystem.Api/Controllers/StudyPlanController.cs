using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class StudyPlanController : BaseController
    {
        public StudyPlanController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var studyPlans = await _unitOfWork.StudyPlans.All();
            return Ok(_mapper.Map<IEnumerable<StudyPlanDto>>(studyPlans));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var studyPlan = await _unitOfWork.StudyPlans.GetById(id);
            if (studyPlan == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StudyPlanDto>(studyPlan));
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudyPlanDto studyPlanDto)
        {
            var studyPlan = _mapper.Map<StudyPlan>(studyPlanDto);
            await _unitOfWork.StudyPlans.Add(studyPlan);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(StudyPlanDto studyPlanDto)
        {
            try
            {
                var studyPlan = _mapper.Map<StudyPlan>(studyPlanDto);
                var result = await _unitOfWork.StudyPlans.Update(studyPlan);
                if (!result)
                {
                    return NotFound();
                }
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
