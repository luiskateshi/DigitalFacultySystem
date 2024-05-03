using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class DegreeProgramController : BaseController
    {
        public DegreeProgramController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetDegreePrograms()
        {
            var degreePrograms = await _unitOfWork.DegreePrograms
                .All();
            return Ok(_mapper.Map<IEnumerable<DegreeProgramDto>>(degreePrograms));
        }

        [HttpGet]
        public async Task<IActionResult> GetDegreeProgram(Guid id)
        {
            var degreeProgram = await _unitOfWork.DegreePrograms.GetById(id);
            if (degreeProgram == null)
            {
                return NotFound("Degree program not found");
            }
            var result = _mapper.Map<DegreeProgramDto>(degreeProgram);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDegreeProgram([FromBody] DegreeProgramDto degreeProgramDto)
        {
            var degreeProgram = _mapper.Map<DegreeProgram>(degreeProgramDto);
            await _unitOfWork.DegreePrograms.Add(degreeProgram);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDegreeProgram(DegreeProgramDto degreeProgramDto)
        {
            var degreeProgramToUpdate = _mapper.Map<DegreeProgram>(degreeProgramDto);
            var result = await _unitOfWork.DegreePrograms.Update(degreeProgramToUpdate);
            if (!result)
            {
                return NotFound("Degree program not found");
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

    }
}
