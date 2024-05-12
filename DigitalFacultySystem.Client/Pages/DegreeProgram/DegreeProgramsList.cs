using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.DegreeProgram
{
    public partial class DegreeProgramsList
    {
        [Inject]
        private IGenericService<DegreeProgramDto> _degreeProgramService { get; set; }
        public IEnumerable<DegreeProgramDto> degreePrograms { get; set; } = new List<DegreeProgramDto>();

        private string apiUrl = "api/DegreeProgram";

        protected override async Task OnInitializedAsync()
        {
            var rows = await _degreeProgramService.GetAll(apiUrl);
            if (rows?.Count != 0)
            {
                degreePrograms = rows;
            }
        }


    }
}
