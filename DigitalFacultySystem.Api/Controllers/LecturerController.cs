using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class LecturerController : BaseController
    {
        public LecturerController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var lecturers = await _unitOfWork.Lecturers.All();
            return Ok(_mapper.Map<IEnumerable<LecturerDto>>(lecturers));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lecturer = await _unitOfWork.Lecturers.GetById(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LecturerDto>(lecturer));
        }

        [HttpPost]
        public async Task<IActionResult> Add(LecturerDto lecturerDto)
        {
            var lecturer = _mapper.Map<Lecturer>(lecturerDto);
            await _unitOfWork.Lecturers.Add(lecturer);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(LecturerDto lecturerDto)
        {
            var lecturer = _mapper.Map<Lecturer>(lecturerDto);
            var result = await _unitOfWork.Lecturers.Update(lecturer);
            if (!result)
            {
                return NotFound();
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}
