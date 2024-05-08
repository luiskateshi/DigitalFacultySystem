using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.StudyPlan
{
    public partial class StudyPlanSubject
    {
        [Inject]
        public IGenericService<StudyPlanSubjectDto> _planSubjectService { get; set; }
        public StudyPlanSubjectDto planSubjectModel { get; set; } = new StudyPlanSubjectDto();

        [Inject]
        public NavigationManager _navigationManager { get; set; }


        [Parameter]
        public string Id { get; set; }
        private String Message { get; set; }

        private string apiUrl = "api/studyPlanSubject";


        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var response = await _planSubjectService.GetById(Id, apiUrl);
                if (response != null)
                {
                    planSubjectModel = response;
                }

            }
        }

        protected void HandleInvalidSubmit()
        {
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task HandleValidSubmit()
        {
            //only update
            var response = await _planSubjectService.Update(planSubjectModel, apiUrl);
            if (response != null)
            {
                //get back
                _navigationManager.NavigateTo("/studyPlan/" + planSubjectModel.StudyPlanId);

            }
        }

        protected async Task Delete()
        {
            var response = await _planSubjectService.Delete(planSubjectModel.Id, apiUrl);
            if (response)
            {
                //get back
                _navigationManager.NavigateTo("/studyPlan/" + planSubjectModel.StudyPlanId);
            }
        }
        //generate course from study plan button click
        protected async Task GenerateCourse()
        {
            var response = await _planSubjectService.Add(planSubjectModel, "api/course/fromStudyPlanSubject");
            //check if bad request
            if (response)
            {
                Message = "Course generated successfully!";
            }
            else
            {
                Message = "Course was already generated for this study plan subject!";
            }

        }

    }
}
