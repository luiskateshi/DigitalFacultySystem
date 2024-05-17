using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Department
{
    public partial class Department
    {
        private AlertCard alertCard;
        [Inject]
        public IGenericService<DepartmentDto> _departmentService { get; set; }

        [Parameter]
        public String Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public DepartmentDto departmentModel { get; set; } = new();

        public ICollection<DegreeProgramDto> degreePrograms { get; set; } = new List<DegreeProgramDto>();

        public String Message { get; set; } = string.Empty;

        private string url = "api/Department";

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var department = await _departmentService.GetById(Id, url);
                degreePrograms = department.DegreePrograms;

                if (department != null)
                    departmentModel = department;
            }
        }

        protected void HandleInvalidSubmit()
        {
            alertCard.ShowAlert("Ka gabime validimi. Ju lutemi, provoni përsëri.", "alert-danger");
        }

        //go back to the departments page
        protected void GoBack()
        {
            _navi.NavigateTo("/departments");
        }
        protected async Task HandleValidSubmit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newDepartment = new DepartmentDto();
                MyFieldsMapper.MapFields(departmentModel, newDepartment);
                var result = await _departmentService.Add(newDepartment, url);
                if (result != null)
                {
                    alertCard.ShowAlert("Departamenti u shtua me sukses!", "alert-success");
                }
                else
                {
                    alertCard.ShowAlert("Shtimi i departamentit dështoi!", "alert-danger");
                }
            }
            else
            {
                var updateDepartment = new DepartmentDto();
                MyFieldsMapper.MapFields(departmentModel, updateDepartment);
                var result = await _departmentService.Update(updateDepartment, url);
                if (result != null)
                {
                    alertCard.ShowAlert("Departamenti u përditësua me sukses!", "alert-success");
                }
                else
                {
                    alertCard.ShowAlert("Përditësimi i departamentit dështoi!", "alert-danger");
                }
            }
        }
    }
}
