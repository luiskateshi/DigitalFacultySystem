using DigitalFacultySystem.ClientApp.Services.Interfaces;
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

        public String Message { get; set; } = string.Empty;

        private string url = "api/Department";

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var department = await _departmentService.GetById(Id, url);

                if(department != null)
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
                var newDepartment = new DepartmentDto()
                {
                    Name = departmentModel.Name
                };
                var result = await _departmentService.Add(newDepartment, url);
                if(result != null)
                    _navi.NavigateTo("/departments");

                Message = "Failed to add department";

            }
            else
            {
                var updateDepartment = new DepartmentDto()
                {
                    Id = departmentModel.Id,
                    Name = departmentModel.Name
                };
                var result = await _departmentService.Update(updateDepartment, url);
                if(result != null)
                    _navi.NavigateTo("/departments");

                Message = "Failed to update department";
            }
        }
    }
}
