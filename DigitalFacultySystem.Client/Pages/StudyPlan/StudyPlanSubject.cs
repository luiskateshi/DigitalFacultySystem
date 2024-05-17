using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;

namespace DigitalFacultySystem.Client.Pages.StudyPlan
{
    public partial class StudyPlanSubject
    {
        private AlertCard _alertCard;
        [Inject]
        public IGenericService<StudyPlanSubjectDto> _planSubjectService { get; set; }
        public StudyPlanSubjectDto planSubjectModel { get; set; } = new StudyPlanSubjectDto();

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }
        private string Message { get; set; }

        private string apiUrl = "api/studyPlanSubject";

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var id = Guid.Parse(this.Id);
                var response = await _planSubjectService.GetById(id, apiUrl);
                if (response != null)
                {
                    planSubjectModel = response;
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            Message = "Ka disa gabime validimi. Ju lutem provoni përsëri.";
            _alertCard.ShowAlert(Message, "alert-danger");
        }

        protected async Task HandleValidSubmit()
        {
            try
            {

                var response = await _planSubjectService.Update(planSubjectModel, apiUrl);
                if (response != null)
                {
                    _alertCard.ShowAlert("Ndryshimet u ruajtën me sukses!", "alert-success");
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                _alertCard.ShowAlert(Message, "alert-danger");
            }
        }

        protected async Task Delete()
        {
            var response = await _planSubjectService.Delete(planSubjectModel.Id, apiUrl);
            if (response)
            {
                _alertCard.ShowAlert("Lënda u fshi me sukses!", "alert-success");
            }
        }

        protected async Task GenerateCourse()
        {
            var response = await _planSubjectService.Add(planSubjectModel, "api/course/fromStudyPlanSubject");
            if (response)
            {
                Message = "Kursi u gjenerua me sukses!";
                _alertCard.ShowAlert(Message, "alert-success");
            }
            else
            {
                Message = "Kursi është gjeneruar tashmë për këtë lëndë të planit të studimit!";
                _alertCard.ShowAlert(Message, "alert-danger");
            }
        }

        private void GoBack()
        {
            _navigationManager.NavigateTo("/StudyPlan/"+planSubjectModel.StudyPlanId);
        }
    }
}
