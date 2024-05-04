using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.ClientApp.Pages.Group
{
    public partial class Group
    {
        [Inject]
        public IGenericService<GroupDto> _groupService { get; set; }

        [Inject]
        public IGenericService<GenerationDto> _generationService { get; set; }

        public List<GenerationDto> generations = new List<GenerationDto>();
        [Inject]
        public NavigationManager _navi { get; set; }

        public GroupDto groupModel = new GroupDto();
        [Parameter]
        public string Id { get; set; }
        public String Message { get; set; }
        public string apiUrl = "api/group";

        protected override async Task OnInitializedAsync()
        {
            generations = await _generationService.GetAll("api/generation");
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var response = await _groupService.GetById(Id, apiUrl);
                if (response != null)
                {
                    groupModel = response;
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task HandleValidSubmit()
        {

            if (string.IsNullOrEmpty(Id))
            {
                var newGroup = new GroupDto();
                MyFieldsMapper.MapFields(groupModel, newGroup);
                var response = await _groupService.Add(newGroup, apiUrl);
                if (response != null)
                {
                    _navi.NavigateTo("/groups");
                }
            }
            else
            {
                var updateGroup = new GroupDto();
                MyFieldsMapper.MapFields(groupModel, updateGroup);
                var response = await _groupService.Update(groupModel, apiUrl);
                if (response != null)
                {
                    _navi.NavigateTo("/groups");
                }
            }
        }
    }
}
