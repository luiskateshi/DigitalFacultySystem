using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using DigitalFacultySystem.Entities;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.ExamSession
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

        private AlertCard alertCard;

        protected override async Task OnInitializedAsync()
        {
            academicYears = await _academicYearService.GetAll("api/academicYear");
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var examSession = await _examSessionService.GetById(Id, "api/examsSession");

                if (examSession != null)
                    examSessionModel = examSession;

                await RefreshExams();
            }
        }

        protected async Task RefreshExams()
        {
            var Id = Guid.Parse(this.Id);
            exams = await _examService.GetAllById(Id, "api/exam/GetExamsByExamSession");
        }

        protected void HandleInValidSumbit()
        {
            alertCard.ShowAlert("Ka disa gabime në validim. Ju lutemi provoni përsëri.", "alert-danger");
        }

        protected async Task HandleValidSumbit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var result = await _examSessionService.Add(examSessionModel, "api/examsSession");
                if (result != null)
                {
                    alertCard.ShowAlert("Sezoni i provimeve u shtua me sukses!", "alert-success");
                    _navi.NavigateTo("/examSessions");
                }
                else
                {
                    alertCard.ShowAlert("Shtimi i sezonit të provimeve dështoi.", "alert-danger");
                }
            }
            else
            {
                var result = await _examSessionService.Update(examSessionModel, "api/examsSession");
                if (result != null)
                {
                    alertCard.ShowAlert("Sezoni i provimeve u përditësua me sukses!", "alert-success");
                    _navi.NavigateTo("/examSessions");
                }
                else
                {
                    alertCard.ShowAlert("Përditësimi i sezonit të provimeve dështoi.", "alert-danger");
                }
            }
        }

        protected async Task GenerateStudentsInCourses()
        {
            var Id = Guid.Parse(this.Id);
            var response = await _examSessionService.ExecuteProcessById(Id, "api/examsSession/GenerateExamsAndStudentsInExams");
            if (response)
            {
                await RefreshExams();
                alertCard.ShowAlert("Procesi u ekzekutua me sukses. Të gjitha provimet u gjeneruan automatikisht për këtë sezon provimesh. Si dhe studentët që duhet të ndjekin provimin përkatës", "alert-success");
            }
            else
            {
                alertCard.ShowAlert("Procedura e ruajtur nuk u ekzekutua me sukses.", "alert-danger");
            }
        }

        protected async Task EndExamSession()
        {
            var Id = Guid.Parse(this.Id);
            var response = await _examSessionService.ExecuteProcessById(Id, "api/examsSession/EndExamSession");
            if (response)
            {
                alertCard.ShowAlert("Sezoni i provimeve përfundoi me sukses.", "alert-success");
            }
            else
            {
                alertCard.ShowAlert("Procedura e ruajtur nuk u ekzekutua me sukses.", "alert-danger");
            }
        }
    }
}
