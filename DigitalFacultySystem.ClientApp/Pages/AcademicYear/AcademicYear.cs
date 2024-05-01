using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.AcademicYear
{
    public partial class AcademicYear
    {
        [Inject]
        public IGenericService<AcademicYearDto> _academicYearService { get; set; }

        [Parameter]
        public String Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public AcademicYearDto academicYearModel { get; set; } = new ();

        public String Message { get; set; } = string.Empty;

        private string url = "api/AcademicYear";

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var academicYear = await _academicYearService.GetById(Id, url);

                if(academicYear != null)
                    academicYearModel = academicYear;
            }
        }

        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
        }

        protected async Task HandleValidSumbit()
        {
            if(string.IsNullOrEmpty(Id))
            {
                var newAcademicYear = new AcademicYearDto()
                {
                    Name = academicYearModel.Name,
                    StartDate = academicYearModel.StartDate,
                    EndDate = academicYearModel.EndDate
                };
                var result = await _academicYearService.Add(newAcademicYear, url);
                if(result != null)
                    _navi.NavigateTo("/academicYears");

                Message = "Failed to add academic year";

            }
            else
            {
                var updateAcademicYear = new AcademicYearDto()
                {
                    Id = academicYearModel.Id,
                    Name = academicYearModel.Name,
                    StartDate = academicYearModel.StartDate,
                    EndDate = academicYearModel.EndDate
                };
                var result = await _academicYearService.Update(updateAcademicYear, url);
                if(result != null)
                    _navi.NavigateTo("/academicYears");

                Message = "Failed to update academic year";
            }
        }
    }
}
