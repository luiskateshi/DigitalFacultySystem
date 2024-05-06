using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class StudyPlanSubjectController : BaseController
    {
        public StudyPlanSubjectController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("byStudyPlan")]
        public async Task<IActionResult> GetSubjectsByStudyPlan(Guid id)
        {
            var subjects = await _unitOfWork.StudyPlanSubjects.GetSubjectsByStudyPlan(id);
            var result = _mapper.Map<IEnumerable<StudyPlanSubjectDto>>(subjects);
            return Ok(result);
        }

        [HttpGet("notInStudyPlan")]
        public async Task<IActionResult> GetSubjectsNotInStudyPlan(Guid id)
        {
            var subjects = await _unitOfWork.StudyPlanSubjects.GetSubjectsNotInStudyPlan(id);
            var result = _mapper.Map<IEnumerable<SubjectDto>>(subjects);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var studyPlanSubject = await _unitOfWork.StudyPlanSubjects.GetById(id);
            if (studyPlanSubject == null)
            {
                return NotFound("Study plan subject not found");
            }
            var result = _mapper.Map<StudyPlanSubjectDto>(studyPlanSubject);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StudyPlanSubjectDto studyPlanSubjectDto)
        {
                var studyPlanSubject = _mapper.Map<StudyPlanSubject>(studyPlanSubjectDto);
                await _unitOfWork.StudyPlanSubjects.Add(studyPlanSubject);
                await _unitOfWork.CompleteAsync();
                return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWork.StudyPlanSubjects.Delete(id);
            if (!result)
            {
                return NotFound("Study plan subject not found");
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StudyPlanSubjectDto studyPlanSubjectDto)
        {
            var studyPlanSubject = _mapper.Map<StudyPlanSubject>(studyPlanSubjectDto);
            var result = await _unitOfWork.StudyPlanSubjects.Update(studyPlanSubject);
            if (!result)
            {
                return NotFound("Study plan subject not found");
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }           
    }
}
