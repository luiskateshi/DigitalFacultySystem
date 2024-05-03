using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.Subjects
{
    public partial class SubjectsList
    {
        [Inject]
        private IGenericService<SubjectDto> _subjectService { get; set; }
        public IEnumerable<SubjectDto> subjects { get; set; }

        private string apiUrl = "api/subject";

        protected override async Task OnInitializedAsync()
        {
            var rows = await _subjectService.GetAll(apiUrl);
            if (rows != null)
            {
                subjects = rows;
            }
        }

    }
}
