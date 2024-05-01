using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.Student
{
    // This class is belongs to the Students.razor component
    public partial class Students
    {
        [Inject]
        private IStudentService _studentService { get; set; }

        public IEnumerable<StudentResponse> _students { get; set; } = new List<StudentResponse>();

        // This method is called when the component is initialized
        protected override async Task OnInitializedAsync()
        {

            var students = await _studentService.GetStudents();
            if (students?.Count != 0)
            {
                _students = students;
            }
        }

    }
}
