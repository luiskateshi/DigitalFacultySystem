using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Group
{
    public partial class GroupsList
    {
        [Inject]
        private IGenericService<GroupDto> _groupService { get; set; }
        public List<GroupDto> groups = new List<GroupDto>();
        private string apiUrl = "api/group";

        protected override async Task OnInitializedAsync()
        {
            var rows = await _groupService.GetAll(apiUrl);
            if (rows != null)
            {
                groups = rows;
            }
        }


    }
}
