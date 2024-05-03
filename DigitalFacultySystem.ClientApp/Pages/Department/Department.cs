using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.Department
{
    public partial class Department
    {
        [Inject]
        public IGenericService<DepartmentDto> _departmentService { get; set; }

        [Parameter]
        public String Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public DepartmentDto departmentModel { get; set; } = new ();

        public ICollection<DegreeProgramDto> degreePrograms { get; set; } = new List<DegreeProgramDto>();

        public String Message { get; set; } = string.Empty;

        private string url = "api/Department";

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var department = await _departmentService.GetById(Id, url);
                degreePrograms = department.DegreePrograms;

                if (department != null)
                    departmentModel = department;
            }
        }

        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
        }

        protected async Task HandleValidSumbit()
        {
            if(string.IsNullOrEmpty(Id))
            {
                var newDepartment = new DepartmentDto();
                MyFieldsMapper.MapFields(departmentModel, newDepartment);
                var result = await _departmentService.Add(newDepartment, url);
                if(result != null)
                    _navi.NavigateTo("/departments");

                Message = "Failed to add department";

            }
            else
            {
                var updateDepartment = new DepartmentDto();
                MyFieldsMapper.MapFields(departmentModel, updateDepartment);
                var result = await _departmentService.Update(updateDepartment, url);
                if(result != null)
                    _navi.NavigateTo("/departments");

                Message = "Failed to update department";
            }
        }
    }
}
