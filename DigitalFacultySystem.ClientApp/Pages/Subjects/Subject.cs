using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.Subjects
{
    public partial class Subject
    {
        [Inject]
        public NavigationManager _navi { get; set; }

        [Inject]
        public IGenericService<SubjectDto> _subjectService { get; set; }
        [Parameter]
        public string Id { get; set; }
        public SubjectDto SubjectModel { get; set; } = new ();
        public String Message { get; set; }
        public string url = "api/subject";
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var response = await _subjectService.GetById(Id, url);
                if (response != null)
                {
                    SubjectModel = response;
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            Message = "There are some validation errors. Please try again.";
        }

        protected async void HandleValidSubmit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newSubject = new SubjectDto();
                MyFieldsMapper.MapFields(SubjectModel, newSubject);
                var response = await _subjectService.Add(newSubject, url);
                if (response)
                {
                    _navi.NavigateTo("/subjects");
                }
                    Message = "Failed to add new subject";
            }
            else
            {
                var updatedSubject = new SubjectDto();
                MyFieldsMapper.MapFields(SubjectModel, updatedSubject);
                var response = await _subjectService.Update(updatedSubject, url);
                if (response)
                {
                    _navi.NavigateTo("/subjects");
                }
                Message = "Failed to update subject";
            }
        }       


    }
}
