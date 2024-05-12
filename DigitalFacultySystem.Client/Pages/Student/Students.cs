using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Student
{
    // This class is belongs to the Students.razor component
    public partial class Students
    {
        [Inject]
        private IGenericService<StudentDto> _studentService { get; set; }
        public IEnumerable<StudentDto> _students { get; set; } = new List<StudentDto>();

        private string apiUrl = "api/Student";


        protected override async Task OnInitializedAsync()
        {
            var rows = await _studentService.GetAll(apiUrl);
            if (rows?.Count != 0)
            {
                _students = rows;
            }
        }

    }
}
