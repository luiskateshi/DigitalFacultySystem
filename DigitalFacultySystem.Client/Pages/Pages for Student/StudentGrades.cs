using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DigitalFacultySystem.Client.Pages.Pages_for_Student
{
    public partial class StudentGrades
    {
        [Inject]
        public IGenericService<StudentExamGradesDto> _studentGradesService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public List<StudentExamGradesDto> studentExamGradesDtos = new();

        public String StudentUserId { get; set; }

        public String StudentId { get; set; }

        public String Message { get; set; } = string.Empty;

        private AlertCard alertCard;

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.User;
            var IsStudent = user.IsInRole("student");

            if (IsStudent)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                StudentUserId = userIdClaim?.Value;
                await GetMyGrades();
            }
        }

        protected async Task GetMyGrades()
        {
            try
            {
                var response = await _studentGradesService.GetAllById(Guid.Parse(StudentUserId), "api/Student/GetAllMyGrades");
                studentExamGradesDtos = response;
                studentExamGradesDtos = studentExamGradesDtos.OrderBy(x => x.Semester).ToList();

                if (studentExamGradesDtos.Count == 0)
                {
                    Message = "Nuk u gjetën nota për këtë student";
                    alertCard.ShowAlert(Message, "alert-warning");
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                alertCard.ShowAlert(Message, "alert-danger");
            }
        }

        protected async Task GetStudentGrades()
        {
            try
            {
                var response = await _studentGradesService.GetAllById(Guid.Parse(StudentId), "api/Student/GetAllStudentGrades");
                studentExamGradesDtos = response;
                studentExamGradesDtos = studentExamGradesDtos.OrderBy(x => x.Semester).ToList();

                if (studentExamGradesDtos.Count == 0)
                {
                    Message = "Nuk u gjetën nota për këtë student";
                    alertCard.ShowAlert(Message, "alert-warning");
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                alertCard.ShowAlert(Message, "alert-danger");
            }
        }
    }
}
