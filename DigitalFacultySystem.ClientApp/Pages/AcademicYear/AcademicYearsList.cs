using DigitalFacultySystem.ClientApp.Services;
using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;

namespace DigitalFacultySystem.ClientApp.Pages.AcademicYear
{
    public partial class AcademicYearsList
    {
        [Inject]
        private IGenericService<AcademicYearDto> _academicYearService { get; set; }
        public IEnumerable<AcademicYearDto> _academicYears { get; set; } = new List<AcademicYearDto>();
        protected override async Task OnInitializedAsync()
        {
            var rows = await _academicYearService.GetAll();
            if (rows?.Count != 0)
            {
                _academicYears = rows;
            }
        }
    }
}
