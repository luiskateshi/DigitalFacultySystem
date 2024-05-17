using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Client.Pages.StudyPlan
{
    public partial class StudyPlan
    {
        [Inject]
        public IGenericService<StudyPlanDto> _studyPlanService { get; set; }

        [Inject]
        public NavigationManager _navi { get; set; }

        [Inject]
        public IGenericService<DepartmentDto> _degreeProgService { get; set; }

        [Inject]
        public IGenericService<StudyPlanSubjectDto> _studyPlanSubjectService { get; set; }

        [Inject]
        public IGenericService<SubjectDto> _subjectService { get; set; }

        public List<SubjectDto> subjectsNotIn = new List<SubjectDto>();
        public List<StudyPlanSubjectDto> planSubjects = new List<StudyPlanSubjectDto>();

        [Parameter]
        public string Id { get; set; } = string.Empty;
        public StudyPlanDto studyPlanModel { get; set; } = new();
        public string url = "api/StudyPlan";
        public IEnumerable<DepartmentDto> degreePrograms { get; set; } = new List<DepartmentDto>();

        private AlertCard _alertCard;

        protected override async Task OnInitializedAsync()
        {
            degreePrograms = await _degreeProgService.GetAll("api/DegreeProgram");

            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var result = await _studyPlanService.GetById(Id, url);

                if (result != null)
                    studyPlanModel = result;
            }

            await RefreshSubjects();
        }

        protected void HandleInvalidSubmit()
        {
            _alertCard.ShowAlert("Ka disa gabime në validim. Ju lutemi provoni përsëri.", "alert-danger");
        }

        protected async Task HandleValidSubmit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newStudyPlan = new StudyPlanDto();
                MyFieldsMapper.MapFields(studyPlanModel, newStudyPlan);
                var result = await _studyPlanService.Add(newStudyPlan, url);
                if (result != null)
                {
                    _alertCard.ShowAlert("Plani i studimit u shtua me sukses!", "alert-success");
                }
                else
                {
                    _alertCard.ShowAlert("Shtimi i planit të studimit dështoi!", "alert-danger");
                }
            }
            else
            {
                var updateStudyPlan = new StudyPlanDto();
                MyFieldsMapper.MapFields(studyPlanModel, updateStudyPlan);
                var result = await _studyPlanService.Update(updateStudyPlan, url);
                if (result != null)
                {
                    _alertCard.ShowAlert("Plani i studimit u përditësua me sukses!", "alert-success");
                }
                else
                {
                    _alertCard.ShowAlert("Përditësimi i planit të studimit dështoi!", "alert-danger");
                }
            }
        }

        protected async Task RefreshSubjects()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                planSubjects = await _studyPlanSubjectService.GetAllById(studyPlanModel.Id, "api/StudyPlanSubject/byStudyPlan");
                subjectsNotIn = await _subjectService.GetAllById(studyPlanModel.Id, "api/StudyPlanSubject/notInStudyPlan");
            }
        }

        protected async Task AddSubjectToStudyPlan(Guid subjectId)
        {
            var studyPlanSubject = new StudyPlanSubjectDto
            {
                StudyPlanId = studyPlanModel.Id,
                SubjectId = subjectId
            };

            var result = await _studyPlanSubjectService.Add(studyPlanSubject, "api/StudyPlanSubject");
            if (result != null)
                await RefreshSubjects();
        }
    }
}
