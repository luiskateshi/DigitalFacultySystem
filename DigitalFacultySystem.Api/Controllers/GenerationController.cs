using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class GenerationController : BaseController
    {
        public GenerationController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var generations = await _unitOfWork.Generations.All();
            return Ok(_mapper.Map<IEnumerable<GenerationDto>>(generations));
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var generation = await _unitOfWork.Generations.GetById(id);
            if (generation == null)
            {
                return NotFound("Generation not found");
            }
            var result = _mapper.Map<GenerationDto>(generation);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GenerationDto generationDto)
        {
            var generation = _mapper.Map<Generation>(generationDto);
            await _unitOfWork.Generations.Add(generation);
            await _unitOfWork.CompleteAsync();
            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GenerationDto generationDto)
        {
            var generation = _mapper.Map<Generation>(generationDto);
            await _unitOfWork.Generations.Update(generation);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWork.Generations.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

    }
}
