using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.StudyPlan
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
        public String Id { get; set; } = string.Empty;
        public StudyPlanDto studyPlanModel { get; set; } = new();
        public String Message { get; set; } = string.Empty;
        public String url = "api/StudyPlan";
        public IEnumerable<DepartmentDto> degreePrograms { get; set; } = new List<DepartmentDto>();

        

        protected override async Task OnInitializedAsync()
        {
            //get all degree progs for dropdown
            degreePrograms = await _degreeProgService.GetAll("api/DegreeProgram");

            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var result = await _studyPlanService.GetById(Id, url);

                if (result != null)
                    studyPlanModel = result;
            }

            await refreshSubjects();
        }

        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
        }
        protected async Task HandleValidSumbit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newStudyPlan = new StudyPlanDto();
                MyFieldsMapper.MapFields(studyPlanModel, newStudyPlan);
                var result = await _studyPlanService.Add(newStudyPlan, url);
                if (result != null)
                    _navi.NavigateTo("/studyPlans");

                Message = "Failed to add study plan";
            }
            else
            {
                var updateStudyPlan = new StudyPlanDto();
                MyFieldsMapper.MapFields(studyPlanModel, updateStudyPlan);
                var result = await _studyPlanService.Update(updateStudyPlan, url);
                if (result != null)
                    _navi.NavigateTo("/studyPlans");

                Message = "Failed to update study plan";
            }
        }

        protected async Task refreshSubjects()
        {
            if (studyPlanModel.Id != Guid.Empty)
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
                await refreshSubjects();
        }

        

    }
}
