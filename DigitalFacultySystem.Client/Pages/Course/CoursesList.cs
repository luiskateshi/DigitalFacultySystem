using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DigitalFacultySystem.Client.Pages.Course
{
    public partial class CoursesList
    {

        private AlertCard alertCard;
        [Inject]
        public IGenericService<CourseDto> _coursService { get; set; }

        public List<CourseDto> courses { get; set; } = new List<CourseDto>();
        private string apiUrl = "api/course";

        protected override async Task OnInitializedAsync()
        {
            courses = await _coursService.GetAll(apiUrl);
        }

        protected async Task GenerateStudentsInCourses()
        {
            var response = await _coursService.ExecuteProcess(apiUrl + "/generateStudentsInCourses");
            if (response)
            {
                alertCard.ShowAlert("Studentët u gjeneruan me sukses!", "alert-success");
            }
            else
            {
                alertCard.ShowAlert("Gjenerimi i studentëve dështoi!", "alert-danger");
            }

        }

    }
}
