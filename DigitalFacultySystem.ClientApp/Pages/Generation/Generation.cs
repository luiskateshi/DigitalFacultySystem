using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;

namespace DigitalFacultySystem.ClientApp.Pages.Generation
{
    public partial class Generation
    {
        [Inject]    
        private IGenericService<GenerationDto> _generationService { get; set; }
        [Inject]
        public IGenericService<StudyPlanDto> _studyPlanService { get; set; }
        [Inject]
        public IGenericService<AcademicYearDto> _academicYearService { get; set; }
        [Inject]
        public IGenericService<DegreeProgramDto> _degreeProgramService { get; set; }
        [Inject]
        public NavigationManager _navi { get; set; }

        public List<StudyPlanDto> studyPlans = new List<StudyPlanDto>();
        public List<AcademicYearDto> academicYears = new List<AcademicYearDto>();
        public List<DegreeProgramDto> degreePrograms = new List<DegreeProgramDto>();

        [Parameter]
        public string Id { get; set; }
        public GenerationDto generatioModel = new GenerationDto();
        public String Message { get; set; }
        public string apiUrl = "api/generation";

        protected override async Task OnInitializedAsync()
        {
            studyPlans = await _studyPlanService.GetAll("api/studyplan");
            academicYears = await _academicYearService.GetAll("api/academicyear");
            degreePrograms = await _degreeProgramService.GetAll("api/degreeprogram");

            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var response = await _generationService.GetById(Id, apiUrl);
                if (response != null)
                {
                    generatioModel = response;
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task HandleValidSubmit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newGeneration = new GenerationDto();
                MyFieldsMapper.MapFields(generatioModel, newGeneration);
                var response = await _generationService.Add(newGeneration, apiUrl);
                if (response)
                {
                    _navi.NavigateTo("/generations");
                }
                Message = "Failed to add new rec";
            }
            else
            {
                var updateGeneration = new GenerationDto();
                MyFieldsMapper.MapFields(generatioModel, updateGeneration);
                var response = await _generationService.Update(updateGeneration, apiUrl);
                if (response)
                {
                    _navi.NavigateTo("/generations");
                }
                Message = "Failed to update rec";
            }
        }



    }
}
