using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Entities;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.ExamSession
{
    public partial class ExamSession
    {
        [Inject]
        public IGenericService<ExamSessionDto> _examSessionService { get; set; }

        [Inject]
        public IGenericService<AcademicYearDto> _academicYearService { get; set; }

        [Inject]
        public IGenericService<ExamDto> _examService { get; set; }

        public List<ExamDto> exams = new List<ExamDto>();

        public List<AcademicYearDto> academicYears = new List<AcademicYearDto>();

        [Parameter]
        public String Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public ExamSessionDto examSessionModel { get; set; } = new();

        public String Message { get; set; } = string.Empty;

        private string url = "api/examsSession";

        private AlertCard alertCard;
        protected override async Task OnInitializedAsync()
        {
            academicYears = await _academicYearService.GetAll("api/academicYear");
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var examSession = await _examSessionService.GetById(Id, url);

                if (examSession != null)
                    examSessionModel = examSession;

                await RefreshExams();
            }
        }
        //refresh exams
        protected async Task RefreshExams()
        {
            var Id = Guid.Parse(this.Id);
            exams = await _examService.GetAllById(Id, "api/exam/GetExamsByExamSession");
        }

        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
        }

        protected async Task HandleValidSumbit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var result = await _examSessionService.Add(examSessionModel, url);
                if (result != null)
                    _navi.NavigateTo("/examSessions");

                Message = "Failed to add exam Sessions";
            }
            else
            {
                var result = await _examSessionService.Update(examSessionModel, url);
                if (result != null)
                    _navi.NavigateTo("/examSessions");

                Message = "Failed to update exam Sessions";
            }
        }

        //call stored procedure to auto generate exams for this exams session
        protected async Task GenerateStudentsInCourses()
        {
            var Id = Guid.Parse(this.Id);
            var response = await _examSessionService.ExecuteProcessById(Id, url + "/GenerateExamsAndStudentsInExams");
            if (response)
            {
                await RefreshExams();
                ShowSuccessAlert(Message = "The stored procedure has been executed successfully. All exams have been generated for this exam session based on the courses that have at least one student who has attended the course but has not yet passed the exam. Additionally, all exams have been populated with students eligible to take the exam.");
            }
            else
            {
                ShowDangerAlert();
            }
        }

        //call stored procedure to end exam session
        protected async Task EndExamSession()
        {
            var Id = Guid.Parse(this.Id);
            var response = await _examSessionService.ExecuteProcessById(Id, url + "/EndExamSession");
            if (response)
            {
                ShowSuccessAlert(Message = "Exam session ended successfully.");
            }
            else
            {
                ShowDangerAlert();
            }
        }

        public void ShowSuccessAlert(String message)
        {
            // Assuming you have an instance of AlertCard named alertCard
            alertCard.ShowAlert(message, "alert-success");
        }

        public void ShowDangerAlert()
        {
            // Assuming you have an instance of AlertCard named alertCard
            alertCard.ShowAlert("The stored procedure encountered an error and could not execute successfully.", "alert-danger");
        }
    }
}
