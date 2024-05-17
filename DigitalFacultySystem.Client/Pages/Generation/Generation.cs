using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Client.Pages.Generation
{
    public partial class Generation
    {
        private AlertCard _alertCard;

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
                else
                {
                    _alertCard.ShowAlert("Nuk u gjet gjenerata", "alert-danger");
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            _alertCard.ShowAlert("Ka gabime validimi. Ju lutemi, provoni përsëri.", "alert-danger");
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
                    _alertCard.ShowAlert("Gjenerata u shtua me sukses!", "alert-success");
                    _navi.NavigateTo("/generations");
                }
                else
                {
                    _alertCard.ShowAlert("Shtimi i gjeneratës dështoi.", "alert-danger");
                }
            }
            else
            {
                var updateGeneration = new GenerationDto();
                MyFieldsMapper.MapFields(generatioModel, updateGeneration);
                var response = await _generationService.Update(updateGeneration, apiUrl);
                if (response)
                {
                    _alertCard.ShowAlert("Gjenerata u përditësua me sukses!", "alert-success");
                    _navi.NavigateTo("/generations");
                }
                else
                {
                    _alertCard.ShowAlert("Përditësimi i gjeneratës dështoi.", "alert-danger");
                }
            }
        }
    }
}
