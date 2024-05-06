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

        public List<AcademicYearDto> academicYears = new List<AcademicYearDto>();

        [Parameter]
        public String Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public ExamSessionDto examSessionModel { get; set; } = new();

        public String Message { get; set; } = string.Empty;

        private string url = "api/examsSession";

        protected override async Task OnInitializedAsync()
        {
            academicYears = await _academicYearService.GetAll("api/academicYear");
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var examSession = await _examSessionService.GetById(Id, url);

                if (examSession != null)
                    examSessionModel = examSession;
            }
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
    }
}
