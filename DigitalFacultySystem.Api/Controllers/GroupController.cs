using AutoMapper;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFacultySystem.Api.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var groups = await _unitOfWork.Groups.All();
            return Ok(_mapper.Map<IEnumerable<GroupDto>>(groups));
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var group = await _unitOfWork.Groups.GetById(id);
            if (group == null)
            {
                return NotFound("Group not found");
            }
            var result = _mapper.Map<GroupDto>(group);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GroupDto groupDto)
        {
            var group = _mapper.Map<Group>(groupDto);
            await _unitOfWork.Groups.Add(group);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GroupDto groupDto)
        {
            var group = _mapper.Map<Group>(groupDto);
            await _unitOfWork.Groups.Update(group);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

    }
}
