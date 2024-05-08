using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DigitalFacultySystem.ClientApp.Pages.Course
{
    public partial class CoursesList
    {
        [Inject]
        public IGenericService<CourseDto> _coursService { get; set; }

        //inject JSRuntime
        [Inject]
        public IJSRuntime JsRuntime { get; set; }
        public List<CourseDto> courses { get; set; } = new List<CourseDto>();
        private string apiUrl = "api/course";

        protected override async Task OnInitializedAsync()
        {
            courses = await _coursService.GetAll(apiUrl);
        }
        object message = "Students have been generated in courses";

        private AlertCard alertCard;
        protected async Task GenerateStudentsInCourses()
        {
            var response = await _coursService.ExecuteProcess(apiUrl + "/generateStudentsInCourses");
            if (response)
            {
                ShowSuccessAlert();
            }
            else
            {
                ShowDangerAlert();
            }

            
        }

        public void ShowSuccessAlert()
        {
            // Assuming you have an instance of AlertCard named alertCard
            alertCard.ShowAlert("A stored procedure executed successfully. All upcoming students will now be part of the courses that the study plan of their generation is connected to.", "alert-success");
        }

        public void ShowWarningAlert()
        {
            // Assuming you have an instance of AlertCard named alertCard
            alertCard.ShowAlert("Warning message goes here.", "alert-warning");
        }

        public void ShowDangerAlert()
        {
            // Assuming you have an instance of AlertCard named alertCard
            alertCard.ShowAlert("Danger message goes here.", "alert-danger");
        }




    }
}
