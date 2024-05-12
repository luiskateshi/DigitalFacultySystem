using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;

namespace DigitalFacultySystem.Client.Pages.ExamRetakeRequest
{
    public partial class ExamRetakeRequests
    {
        [Inject]
        private IGenericService<ExamRetakeRequestDto> _examRetakeRequestService { get; set; }
        [Inject]
        private IGenericService<PossibleExamRetakesDto> _possibleExamRetakesService { get; set; }
        private ExamRetakeRequestDto requestModel = new ExamRetakeRequestDto();
        private List<ExamRetakeRequestDto> requests = new List<ExamRetakeRequestDto>();
        private List<PossibleExamRetakesDto> possibleExamRetakes = new List<PossibleExamRetakesDto>();

        private AlertCard alertCard;
        private Guid StudentId { get; set; }
        private String Message { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            StudentId = Guid.Parse("D01B4F5C-9867-4DF0-81C6-1A6FE77FDABE");
            requests = await _examRetakeRequestService.GetAllById(StudentId, "api/examRetakeRequest/GetRequestsByStudent");
            possibleExamRetakes = await _possibleExamRetakesService.GetAllById(StudentId, "api/examRetakeRequest/GetPossibleExamRetakes");
        }


        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
            ShowAlertCardDanger(Message);
        }

        protected async Task HandleValidSumbit()
        {
            if (requests.Count >= 2)
            {
                Message = "The maxiaml number of requests for this academic year has been reached.";
                ShowAlertCardDanger(Message);
                return;
            }
            var result = await _examRetakeRequestService.Add(requestModel, "api/examRetakeRequest");
            if (result)
            {

                Message = "Request added successfully.";
                ShowAlertCardSuccess(Message);
                await RefreshRequests();
            }
        }

        //refresh exam retake requests and possible exam retakes
        protected async Task RefreshRequests()
        {
            requests = await _examRetakeRequestService.GetAllById(StudentId, "api/examRetakeRequest/GetRequestsByStudent");
            possibleExamRetakes = await _possibleExamRetakesService.GetAllById(StudentId, "api/examRetakeRequest/GetPossibleExamRetakes");
        }

        //show alert card success
        protected void ShowAlertCardSuccess(String Message)
        {
            alertCard.ShowAlert(Message, "alert-success");
        }
        //show alert card danger
        protected void ShowAlertCardDanger(String Message)
        {
            alertCard.ShowAlert(Message, "alert-danger");
        }
    }
}
