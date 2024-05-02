using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.DegreeProgram
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

        private string url = "api/DegreeProgram";

        protected override async Task OnInitializedAsync()
        {
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
                var newDegree = new DegreeProgramDto()
                {
                    Name = degreeModel.Name
                };
                var result = await _degreeService.Add(newDegree, url);
                if (result != null)
                    _navi.NavigateTo("/degreePrograms");

                Message = "Failed to add degree program";

            }
            else
            {
                var updateDegree = new DegreeProgramDto()
                {
                    Id = degreeModel.Id,
                    Grade = degreeModel.Grade,
                    Name = degreeModel.Name
                };
                var result = await _degreeService.Update(updateDegree, url);
                if (result != null)
                    _navi.NavigateTo("/degreePrograms");

                Message = "Failed to update degree program";
            }
        }
    }
}
