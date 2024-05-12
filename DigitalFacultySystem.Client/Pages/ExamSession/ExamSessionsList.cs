using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.ExamSession
{
    public partial class ExamSessionsList
    {
        [Inject]
        public IGenericService<ExamSessionDto> _examSessionService { get; set; }
        public List<ExamSessionDto> examSessions { get; set; } = new ();
        private string apiUrl = "api/examsSession";

        protected override async Task OnInitializedAsync()
        {
            examSessions = await _examSessionService.GetAll(apiUrl);
        }
    }
}
