using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.Course
{
    public partial class CoursesList
    {
        [Inject]
        public IGenericService<CourseDto> _coursService { get; set; }
        public List<CourseDto> courses { get; set; } = new List<CourseDto>();
        private string apiUrl = "api/course";

        protected override async Task OnInitializedAsync()
        {
            courses = await _coursService.GetAll(apiUrl);
        }

    }
}
