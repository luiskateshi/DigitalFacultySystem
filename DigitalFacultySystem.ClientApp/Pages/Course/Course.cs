using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;

namespace DigitalFacultySystem.ClientApp.Pages.Course
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

        public List<LecturerDto> lecturers { get; set; } = new List<LecturerDto>();
        //[Inject]
        //public IGenericService<StudyPlanSubjectDto> _studyPlanSubjectService { get; set; }

        public List<StudyPlanSubjectDto> studyPlanSubjects { get; set; } = new List<StudyPlanSubjectDto>();

        [Parameter]
        public string Id { get; set; }
        private String Message { get; set; }

        private string apiUrl = "api/course";


        protected override async Task OnInitializedAsync()
        {
            lecturers = await _lecturerService.GetAll("api/lecturer");
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var response = await _courseService.GetById(Id, apiUrl);
                if (response != null)
                {
                    courseModel = response;
                }

            }
        }

        protected void HandleInvalidSubmit()
        {
            Message = "There are some validation errors. Please try again.";
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

    }
}
