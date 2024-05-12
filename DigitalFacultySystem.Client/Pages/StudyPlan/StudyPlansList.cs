using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.StudyPlan
{
    public partial class StudyPlansList
    {
        [Inject]
        private IGenericService<StudyPlanDto> _studyPlanService { get; set; }
        public IEnumerable<StudyPlanDto> studyPlans { get; set; } = new List<StudyPlanDto>();

        private string apiUrl = "api/StudyPlan";

        protected override async Task OnInitializedAsync()
        {
            var rows = await _studyPlanService.GetAll(apiUrl);
            if (rows?.Count != 0)
            {
                studyPlans = rows;
            }
        }
    }
}
