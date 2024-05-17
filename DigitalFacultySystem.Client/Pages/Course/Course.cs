using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Client.Pages.Course
{
    public partial class Course
    {
        [Inject]
        private IGenericService<CourseDto> _courseService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private IGenericService<LecturerDto> _lecturerService { get; set; }

        [Inject]
        private IGenericService<StudyPlanSubjectDto> _studyPlanSubjectService { get; set; }

        [Inject]
        private IGenericService<CourseAttendanceDto> _courseAttendanceService { get; set; }

        public CourseDto CourseModel { get; set; } = new CourseDto();
        public List<LecturerDto> Lecturers { get; set; } = new List<LecturerDto>();
        public IEnumerable<CourseAttendanceDto> CourseAttendances { get; set; } = new List<CourseAttendanceDto>();

        [Parameter]
        public string Id { get; set; }

        private string Message { get; set; }
        private const string ApiUrl = "api/course";
        private string CourseSubject { get; set; }
        private AlertCard _alertCard;

        protected override async Task OnInitializedAsync()
        {
            Lecturers = await _lecturerService.GetAll("api/lecturer");

            if (!string.IsNullOrEmpty(Id))
            {
                var courseId = Guid.Parse(Id);
                CourseModel = await _courseService.GetById(courseId, ApiUrl);
                CourseAttendances = await _courseAttendanceService.GetAllById(courseId, "api/course/GetStudentsInCourse");

                if (CourseModel != null)
                {
                    CourseSubject = CourseModel.StudyPlanSubject.Name;
                }

                await RefreshStudentsInCourse();
            }
        }

        protected async Task RefreshStudentsInCourse()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var courseId = Guid.Parse(Id);
                CourseAttendances = await _courseAttendanceService.GetAllById(courseId, "api/course/GetStudentsInCourse");
            }
        }

        protected void HandleInvalidSubmit()
        {
            Message = "Ka disa gabime të validimit. Ju lutemi provoni përsëri.";
        }

        protected async Task Delete()
        {
            if (CourseModel.Id != Guid.Empty)
            {
                var response = await _courseService.Delete(CourseModel.Id, ApiUrl);
                if (response)
                {
                    _navigationManager.NavigateTo("/courses");
                }
                else
                {
                    _alertCard.ShowAlert("Fshirja e kursit dështoi.", "alert-danger");
                }
            }
        }

        protected async Task HandleValidSubmit()
        {
            try
            {
                if (CourseModel.Id == Guid.Empty)
                {
                    var response = await _courseService.Add(CourseModel, ApiUrl);
                    if (response != null)
                    {
                        _alertCard.ShowAlert("Kursi u shtua me sukses!", "alert-success");
                    }
                }
                else
                {
                    var response = await _courseService.Update(CourseModel, ApiUrl);
                    if (response != null)
                    {
                        _alertCard.ShowAlert("Kursi u përditësua me sukses!", "alert-success");
                    }
                }
            }
            catch (Exception ex)
            {
                _alertCard.ShowAlert("Diçka shkoi keq: " + ex.Message, "alert-danger");
            }
        }

        protected async Task SaveCourseAttendances()
        {
            var response = await _courseAttendanceService.UpdateList(CourseAttendances, "api/course/UpdateCourseAttendance");
            if (response)
            {
                _alertCard.ShowAlert("Frekuentimet u përditësuan me sukses", "alert-success");
            }
            else
            {
                _alertCard.ShowAlert("Përditësimi i frekuentimeve dështoi!", "alert-danger");
            }
        }

        protected async Task CalculateCourseAttendance()
        {
            var response = await _courseAttendanceService.ExecuteProcessById(CourseModel.Id, "api/course/CalculateCourseAttendance");
            if (response)
            {
                await RefreshStudentsInCourse();
                _alertCard.ShowAlert("Procedura u ekzekutua me sukses. Statusi për pjesëmarrjen në kurs është përcaktuar bazuar në planin e studimit dhe orët totale për seminarë dhe laboratorë. Për të shënuar kursin si të përfunduar për një student, nevojiten të paktën 75% e orëve të seminarëve dhe 100% e orëve të laboratorit të frekuentuara.", "alert-success");
            }
            else
            {
                _alertCard.ShowAlert("Procesi për kalkulimin e frekuentimeve të kursit dështoi!", "alert-danger");
            }
        }
    }
}
