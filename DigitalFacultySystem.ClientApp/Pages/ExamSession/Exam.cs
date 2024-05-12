using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.ExamSession
{
    public partial class Exam
    {
        [Inject]
        public IGenericService<ExamDto> _examService { get; set; }
        [Inject]
        public IGenericService<StudentsInExamDto> _studentInExamService { get; set; }

        public List<StudentsInExamDto> studentsInExam = new ();

        public ExamDto examModel { get; set; } = new();

        [Parameter]
        public string Id { get; set; } = string.Empty;

        public String Message { get; set; } = string.Empty;

        private string url = "api/exam";

        private AlertCard alertCard;


        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var exam = await _examService.GetById(Id, url);

                if (exam != null)
                    examModel = exam;

                await RefreshStudentsInExam();
            }
        }
        //refresh students in exam
        protected async Task RefreshStudentsInExam()
        {
            var Id = Guid.Parse(this.Id);
            studentsInExam = await _studentInExamService.GetAllById(Id, "api/exam/GetStudentsInExam");
        }

        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
        }

        protected async Task HandleValidSumbit()
        {
            var result = await _examService.Update(examModel, url);
            if (result)
            {
                Message = "Exam updated successfully.";
                alertCard.ShowAlert(Message, "alert-success");
            }
            else
            {
                Message = "There was an error updating the exam. Please try again.";
                alertCard.ShowAlert(Message, "alert-danger");
            }
        }

        //update students in exam
        protected async Task UpdateStudentsInExam()
        {
            var result = await _studentInExamService.UpdateList(studentsInExam, "api/exam/UpdateStudentsInExam");
            if (result)
            {
                Message = "Students grades and their attendance for this exam was updated successfully.";
                alertCard.ShowAlert(Message, "alert-success");
            }
            else
            {
                Message = "There was an error updating the students in exam. Please try again.";
                alertCard.ShowAlert(Message, "alert-danger");
            }
        }

    }
}
