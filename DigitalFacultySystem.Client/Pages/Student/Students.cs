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

        private string searchTerm;


        protected override async Task OnInitializedAsync()
        {
            await LoadStudents();
        }

        private async Task LoadStudents()
        {
            _students = await _studentService.GetAll("api/student");
        }

        private async Task SearchStudents(ChangeEventArgs e)
        {
            searchTerm = e.Value.ToString();
            if (string.IsNullOrEmpty(searchTerm))
            {
                await LoadStudents();
            }
            else
            {
                _students = await _studentService.Search(searchTerm, "api/student/search");
            }
        }

    }
}
