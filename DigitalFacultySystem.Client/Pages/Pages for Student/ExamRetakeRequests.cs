using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;

namespace DigitalFacultySystem.Client.Pages.Pages_for_Student
{
    public partial class ExamRetakeRequests
    {
        public ExamRetakeRequestDto requestModel { get; set; } = new();
        public List<ExamRetakeRequestDto> requests { get; set; } = new();
        public List<PossibleExamRetakesDto> possibleExamRetakes { get; set; } = new();
        private AlertCard alertCard;
        private Guid StudentId { get; set; }
        public String Message { get; set; } = string.Empty;

        [Inject] private IGenericService<ExamRetakeRequestDto> _examRetakeRequestService { get; set; }
        [Inject] private IGenericService<PossibleExamRetakesDto> _possibleExamRetakesService { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.User;
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = Guid.Parse(userIdClaim?.Value);

            StudentId = await _examRetakeRequestService.GetStudentIdByUserId(UserId, "api/Student/GetStudentByUserId");

            await RefreshRequests();
        }

        protected void HandleInValidSumbit()
        {
            Message = "Ka disa gabime në validim. Ju lutemi provoni përsëri.";
            ShowAlertCardDanger(Message);
        }

        protected async Task HandleValidSumbit()
        {
            if(requestModel.ExamId == null)
            {
                Message = "Zgjidhni një provim.";
                ShowAlertCardDanger(Message);
                return;
            }
            if (requests.Count >= 2)
            {
                Message = "Numri maksimal i kërkesave për këtë vit akademik është arritur.";
                ShowAlertCardDanger(Message);
                return;
            }
            requestModel.StudentId = StudentId;
            var result = await _examRetakeRequestService.Add(requestModel, "api/examRetakeRequest");
            if (result)
            {
                Message = "Kërkesa u shtua me sukses.";
                ShowAlertCardSuccess(Message);
                await RefreshRequests();
            }
        }

        protected async Task RefreshRequests()
        {
            requests = await _examRetakeRequestService.GetAllById(StudentId, "api/examRetakeRequest/GetRequestsByStudent");
            possibleExamRetakes = await _possibleExamRetakesService.GetAllById(StudentId, "api/examRetakeRequest/GetPossibleExamRetakes");
        }

        protected void ShowAlertCardSuccess(String message)
        {
            alertCard.ShowAlert(message, "alert-success");
        }

        protected void ShowAlertCardDanger(String message)
        {
            alertCard.ShowAlert(message, "alert-danger");
        }
    }
}