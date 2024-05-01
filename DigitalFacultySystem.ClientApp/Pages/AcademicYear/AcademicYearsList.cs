using DigitalFacultySystem.ClientApp.Services;
using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.AcademicYear
{
    public partial class AcademicYearsList
    {
        [Inject]
        private IGenericService<AcademicYearDto> _academicYearService { get; set; }
        public IEnumerable<AcademicYearDto> _academicYears { get; set; } = new List<AcademicYearDto>();

        private string apiUrl = "api/AcademicYear";


        protected override async Task OnInitializedAsync()
        {
            var rows = await _academicYearService.GetAll(apiUrl);
            if (rows?.Count != 0)
            {
                _academicYears = rows;
            }
        }
    }
}
