using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.DegreeProgram
{
    public partial class DegreeProgram
    {
        private AlertCard alertCard;
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
            alertCard.ShowAlert("Gabime ne validimin e formes!", "alert-danger");
        }

        protected async Task HandleValidSumbit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newDegree = new DegreeProgramDto();
                MyFieldsMapper.MapFields(degreeModel, newDegree);
                
                var result = await _degreeService.Add(newDegree, url);
                if (result != null)
                    alertCard.ShowAlert("Programi i studimit u shtua me sukses!", "alert-success");
                else
                    alertCard.ShowAlert("Programi i studimit nuk u shtua!", "alert-danger");
            }
            else
            {
                var updateDegree = new DegreeProgramDto();
                MyFieldsMapper.MapFields(degreeModel, updateDegree);

                var result = await _degreeService.Update(updateDegree, url);
                if (result != null)
                    alertCard.ShowAlert("Programi i studimit u përditësua me sukses!", "alert-success");
                else
                    alertCard.ShowAlert("Programi i studimit nuk u përditësua!", "alert-danger");
            }
        }
        private void GoBack()
        {
            NavManager.NavigateTo("/DegreePrograms");
        }
    }
}
