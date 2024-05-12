using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.DegreeProgram
{
    public partial class DegreeProgram
    {
        [Inject]
        public IGenericService<DegreeProgramDto> _degreeService { get; set; }

        [Parameter]
        public String Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public DegreeProgramDto degreeModel { get; set; } = new();

        public String Message { get; set; } = string.Empty;

        public IEnumerable<DepartmentDto> departments { get; set; } = new List<DepartmentDto>();

        [Inject]
        public IGenericService<DepartmentDto> _departmentService { get; set; }

        private string url = "api/DegreeProgram";

        protected override async Task OnInitializedAsync()
        {
            //get all departments for dropdown
            departments = await _departmentService.GetAll("api/Department");

            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var degree = await _degreeService.GetById(Id, url);
                
                if (degree != null)
                    degreeModel = degree;
            }
        }

        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
        }

        protected async Task HandleValidSumbit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newDegree = new DegreeProgramDto();
                MyFieldsMapper.MapFields(degreeModel, newDegree);
                
                var result = await _degreeService.Add(newDegree, url);
                if (result != null)
                    _navi.NavigateTo("/degreePrograms");

                Message = "Failed to add degree program";

            }
            else
            {
                var updateDegree = new DegreeProgramDto();
                MyFieldsMapper.MapFields(degreeModel, updateDegree);

                var result = await _degreeService.Update(updateDegree, url);
                if (result != null)
                    _navi.NavigateTo("/degreePrograms");

                Message = "Failed to update degree program";
            }
        }
    }
}
