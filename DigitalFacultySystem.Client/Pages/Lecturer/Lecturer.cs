using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Lecturer
{
    public partial class Lecturer
    {
        [Inject]
        public NavigationManager _navi { get; set; }

        [Inject]
        public IGenericService<LecturerDto> _lecturerService { get; set; }

        public LecturerDto lecturerModel { get; set; } = new LecturerDto();

        [Parameter]
        public string Id { get; set; } = string.Empty;

        private AlertCard _alertCard;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var result = await _lecturerService.GetById(Id, "api/Lecturer");
                if (result != null)
                {
                    lecturerModel = result;
                }
                else
                {
                    _alertCard.ShowAlert("Ligjëruesi nuk u gjet", "alert-danger");
                }
            }
        }

        protected void HandleInValidSumbit()
        {
            _alertCard.ShowAlert("Ka disa gabime në validim. Ju lutemi provoni përsëri.", "alert-danger");
        }

        protected async Task HandleValidSumbit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var newlecturer = new LecturerDto();
                MyFieldsMapper.MapFields(lecturerModel, newlecturer);
                var result = await _lecturerService.Add(newlecturer, "api/Lecturer");
                if (result != null)
                {
                    _alertCard.ShowAlert("Pedagogu u shtua me sukses!", "alert-success");
                }
                else
                {
                    _alertCard.ShowAlert("Shtimi i pedagogut dështoi.", "alert-danger");
                }
            }
            else
            {
                var updateLecturer = new LecturerDto();
                MyFieldsMapper.MapFields(lecturerModel, updateLecturer);
                var result = await _lecturerService.Update(updateLecturer, "api/Lecturer");
                if (result)
                {
                    _alertCard.ShowAlert("Pedagogu u përditësua me sukses!", "alert-success");
                }
                else
                {
                    _alertCard.ShowAlert("Përditësimi i pedagogut dështoi.", "alert-danger");
                }
            }
        }
    }
}
