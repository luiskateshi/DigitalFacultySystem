using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Client.Services;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Department
{
    public partial class DepartmentsList
    {
        [Inject]
        private IGenericService<DepartmentDto> _deparmentService { get; set; }
        public IEnumerable<DepartmentDto> departments { get; set; } = new List<DepartmentDto>();

        private string apiUrl = "api/Department";


        protected override async Task OnInitializedAsync()
        {
            var rows = await _deparmentService.GetAll(apiUrl);
            if (rows?.Count != 0)
            {
                departments = rows;
            }
        }
    }

}
