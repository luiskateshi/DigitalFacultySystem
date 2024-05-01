using Microsoft.AspNetCore.Mvc;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using AutoMapper;

namespace DigitalFacultySystem.Api.Controllers
{
    public class DepartmentController : BaseController
    {
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _unitOfWork.Departments.All();
            var result = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            var department = await _unitOfWork.Departments.GetById(id);
            if (department == null)
            {
                return NotFound("Department not found");
            }
            var result = _mapper.Map<DepartmentDto>(department);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            await _unitOfWork.Departments.Add(department);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto departmentDto)
        {
            var departmentToUpdate = _mapper.Map<Department>(departmentDto);
            var result = await _unitOfWork.Departments.Update(departmentToUpdate);
            if (!result)
            {
                return NotFound("Department not found");
            }
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}
