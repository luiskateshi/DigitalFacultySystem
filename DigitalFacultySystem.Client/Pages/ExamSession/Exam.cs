using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.ExamSession
{
    public partial class Exam
    {
        [Inject]
        public IGenericService<ExamDto> _examService { get; set; }
        [Inject]
        public IGenericService<StudentsInExamDto> _studentInExamService { get; set; }

        public List<StudentsInExamDto> studentsInExam = new();

        public ExamDto examModel { get; set; } = new();

        [Parameter]
        public string Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        private AlertCard alertCard;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var exam = await _examService.GetById(Id, "api/exam");

                if (exam != null)
                    examModel = exam;

                await RefreshStudentsInExam();
            }
        }

        protected async Task RefreshStudentsInExam()
        {
            var Id = Guid.Parse(this.Id);
            studentsInExam = await _studentInExamService.GetAllById(Id, "api/exam/GetStudentsInExam");
        }

        protected void HandleInValidSumbit()
        {
            alertCard.ShowAlert("Ka disa gabime në validim. Ju lutemi provoni përsëri.", "alert-danger");
        }

        protected async Task HandleValidSumbit()
        {
            var result = await _examService.Update(examModel, "api/exam");
            if (result)
            {
                alertCard.ShowAlert("Provimi u përditësua me sukses.", "alert-success");
            }
            else
            {
                alertCard.ShowAlert("Pati një gabim gjatë përditësimit të provimit. Ju lutemi provoni përsëri.", "alert-danger");
            }
        }

        protected async Task UpdateStudentsInExam()
        {
            var result = await _studentInExamService.UpdateList(studentsInExam, "api/exam/UpdateStudentsInExam");
            if (result)
            {
                alertCard.ShowAlert("Notat dhe prezenca e studentëve në këtë provim u përditësuan me sukses.", "alert-success");
            }
            else
            {
                alertCard.ShowAlert("Pati një gabim gjatë përditësimit të studentëve në provim. Ju lutemi provoni përsëri.", "alert-danger");
            }
        }
    }
}
