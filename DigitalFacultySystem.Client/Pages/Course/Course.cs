using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;

namespace DigitalFacultySystem.Client.Pages.Course
{
    public partial class Course
    {
        [Inject]
        
        public IGenericService<CourseDto> _courseService { get; set; }
        public CourseDto courseModel { get; set; } = new CourseDto();

        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public IGenericService<LecturerDto> _lecturerService { get; set; }

        [Inject]
        public IGenericService<StudyPlanSubjectDto> _studyPlanSubjectService { get; set; }

        [Inject]//courseAttendance
        public IGenericService<CourseAttendanceDto> _courseAttendanceService { get; set; }


        public List<LecturerDto> lecturers { get; set; } = new ();

        public IEnumerable<CourseAttendanceDto> courseAttendances { get; set; } = new List<CourseAttendanceDto>();


        [Parameter]
        public string Id { get; set; }
        private String Message { get; set; }

        private string apiUrl = "api/course";
        private String CourseSubject { get; set; }

        private AlertCard alertCard;

        protected override async Task OnInitializedAsync()
        {
            lecturers = await _lecturerService.GetAll("api/lecturer");

            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var response = await _courseService.GetById(Id, apiUrl);
                courseAttendances = await _courseAttendanceService.GetAllById(Id, "api/course/GetStudentsInCourse");
                if (response != null)
                {
                    courseModel = response;
                    CourseSubject = courseModel.StudyPlanSubject.Name;
                }
                await RefreshStudentsInCourse();

            }
        }
        protected async Task RefreshStudentsInCourse()
        {
            var Id = Guid.Parse(this.Id);
            courseAttendances = await _courseAttendanceService.GetAllById(Id, "api/course/GetStudentsInCourse");
        }

        protected void HandleInvalidSubmit()
        {
            Message = "There are some validation errors. Please try again.";
        }

        //delete course
        protected async Task Delete()
        {
            var response = await _courseService.Delete(courseModel.Id, apiUrl);
            if (response)
            {
                _navigationManager.NavigateTo("/courses");
            }
        }

        protected async Task HandleValidSubmit()
        {
            if (courseModel.Id == Guid.Empty)
            {
                try
                {
                    var response = await _courseService.Add(courseModel, apiUrl);
                    if (response != null)
                    {
                        _navigationManager.NavigateTo("/courses");
                    }
                }
                catch (Exception ex)
                {
                    Message = "There are some validation errors. Please try again.";
                }
                
            }
            else
            {
                var response = await _courseService.Update(courseModel, apiUrl);
                if (response != null)
                {
                    _navigationManager.NavigateTo("/courses");
                }
            }
        }

        //SaveCourseAttendances
        protected async Task SaveCourseAttendances()
        {
            var response = await _courseAttendanceService.UpdateList(courseAttendances, "api/course/UpdateCourseAttendance");
            if (response)
            {
                Message = "Course attendances saved successfully!";
            }
            else
            {
                Message = "Failed to save course attendances!";
            }
        }

        //calculate attendance
        protected async Task CalculateCourseAttendance()
        {
            var response = await _courseAttendanceService.ExecuteProcessById(courseModel.Id, "api/course/CalculateCourseAttendance");
            if (response)
            {
                await RefreshStudentsInCourse();
                ShowSuccessAlert();
            }
            else
            {
                Message = "Failed to calculate course attendance!";
            }
        }
        public void ShowSuccessAlert()
        {
            alertCard.ShowAlert("The stored procedure has been executed successfully. The \"is Attended\" status for the course has been determined based on the study plan subject associated with this course and the total hours allocated for seminars and laboratories. To mark the course as \"attended\" for a student, it is required that at least 75% of the total seminar hours and 100% of the total lab hours have been attended.", "alert-success");
        }

    }
}
