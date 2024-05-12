using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Lecturer
{
    public partial class Lecturer
    {
        [Inject]
        public IGenericService<LecturerDto> _lecturerService { get; set; }

        [Parameter]
        public String Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public LecturerDto lecturerModel { get; set; } = new();

        public String Message { get; set; } = string.Empty;

        private string url = "api/Lecturer";

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var result = await _lecturerService.GetById(Id, url);

                if (result != null)
                    lecturerModel = result;
            }
        }

        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
        }

        protected async Task HandleValidSumbit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newlecturer = new LecturerDto();
                MyFieldsMapper.MapFields(lecturerModel, newlecturer);
                var result = await _lecturerService.Add(newlecturer, url);
                if (result != null)
                    _navi.NavigateTo("/lecturers");

                Message = "Failed to add lecturer";

            }
            else
            {
                var updateLecturer = new LecturerDto();
                MyFieldsMapper.MapFields(lecturerModel, updateLecturer);
                var result = await _lecturerService.Update(updateLecturer, url);
                if (result != null)
                    _navi.NavigateTo("/lecturers");

                Message = "Failed to update lecturer";
            }
        }
    }
}

