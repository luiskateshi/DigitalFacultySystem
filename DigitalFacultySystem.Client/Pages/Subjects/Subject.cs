using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace DigitalFacultySystem.Client.Pages.Subjects
{
    public partial class Subject
    {
        [Inject]
        public NavigationManager _navi { get; set; }

        [Inject]
        public IGenericService<SubjectDto> _subjectService { get; set; }

        public SubjectDto SubjectModel { get; set; } = new SubjectDto();

        [Parameter]
        public string Id { get; set; } = string.Empty;

        private AlertCard _alertCard;

        private string fileName;
        private bool isFileSelected;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var response = await _subjectService.GetById(Id, "api/subject");
                if (response != null)
                {
                    SubjectModel = response;
                }
                else
                {
                    _alertCard.ShowAlert("Lënda nuk u gjet", "alert-danger");
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            _alertCard.ShowAlert("Ka disa gabime në validim. Ju lutemi provoni përsëri.", "alert-danger");
        }

        protected async Task HandleValidSubmit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newSubject = new SubjectDto();
                MyFieldsMapper.MapFields(SubjectModel, newSubject);
                var response = await _subjectService.Add(newSubject, "api/subject");
                if (response)
                {
                    _alertCard.ShowAlert("Lënda u shtua me sukses!", "alert-success");
                    _navi.NavigateTo("/subjects");
                }
                else
                {
                    _alertCard.ShowAlert("Shtimi i lëndës dështoi.", "alert-danger");
                }
            }
            else
            {
                var updatedSubject = new SubjectDto();
                MyFieldsMapper.MapFields(SubjectModel, updatedSubject);
                var response = await _subjectService.Update(updatedSubject, "api/subject");
                if (response)
                {
                    _alertCard.ShowAlert("Lënda u përditësua me sukses!", "alert-success");
                    _navi.NavigateTo("/subjects");
                }
                else
                {
                    _alertCard.ShowAlert("Përditësimi i lëndës dështoi.", "alert-danger");
                }
            }
        }

        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            fileName = file.Name;
            isFileSelected = true;
        }

        private void UploadSyllabus()
        {
            _alertCard.ShowAlert("Syllabusi u ngarkua me sukses!", "alert-success");
            fileName = string.Empty;
            isFileSelected = false;
        }

    }
}
