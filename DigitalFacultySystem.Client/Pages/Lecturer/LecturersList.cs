using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Lecturer
{
    public partial class LecturersList
    {
        [Inject]
        private IGenericService<LecturerDto> _lecturerService { get; set; }
        public IEnumerable<LecturerDto> lecturers { get; set; } = new List<LecturerDto>();

        private string apiUrl = "api/Lecturer";

        protected override async Task OnInitializedAsync()
        {
            var rows = await _lecturerService.GetAll(apiUrl);
            if (rows?.Count != 0)
            {
                lecturers = rows;
            }
        }

    }
}
