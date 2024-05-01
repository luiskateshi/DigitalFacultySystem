using AutoMapper;
using DigitalFacultySystem.DataService.Data;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalFacultySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : BaseController
    {
        public AcademicYearController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAcademicYears()
        {
            var years = await _unitOfWork.AcademicYears.All();
            var result = _mapper.Map<IEnumerable<AcademicYearDto>>(years);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAcademicYear(Guid id)
        {
            var year = await _unitOfWork.AcademicYears.GetById(id);
            if (year == null)
            {
                return NotFound("Academic Year not found");
            }
            var result = _mapper.Map<AcademicYearDto>(year);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAcademicYears([FromBody] AcademicYearDto yearDto)
        {
            var year = _mapper.Map<AcademicYear>(yearDto);
            await _unitOfWork.AcademicYears.Add(year);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAcademicYear(AcademicYearDto yearDto)
        {
            var yearToUpdate = _mapper.Map<AcademicYear>(yearDto);
            var result = await _unitOfWork.AcademicYears.Update(yearToUpdate);
            if (!result)
            {
                return NotFound("Academic Year not found");
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }


    }
}
