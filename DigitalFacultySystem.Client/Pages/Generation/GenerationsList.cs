using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Generation
{
    public partial class GenerationsList
    {
        [Inject]    
        private IGenericService<GenerationDto> _generationService { get; set; }
        public List<GenerationDto> generations = new List<GenerationDto>();

        private string apiUrl = "api/generation";

        protected override async Task OnInitializedAsync()
        {
            var rows = await _generationService.GetAll(apiUrl);
            if (rows != null)
            {
                generations = rows;
            }
        }
    }
}
