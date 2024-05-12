using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;

namespace DigitalFacultySystem.Client.Pages.Group
{
    public partial class Group
    {
        [Inject]
        public IGenericService<GroupDto> _groupService { get; set; }

        [Inject]
        public IGenericService<GenerationDto> _generationService { get; set; }
        [Inject]
        public IGenericService<StudentDto> _studentService { get; set; }
        [Inject]
        public IGenericService<StudentInGroupDto> _studentInGroupService { get; set; }

        public List<StudentDto> studentsWithoutGroup = new List<StudentDto>();
        public List<StudentDto> studentsInGroup = new List<StudentDto>();

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
                await refreshStudents();
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
                var response = await _groupService.Add(groupModel, apiUrl);
                if (response != null)
                {
                    _navi.NavigateTo("/groups");
                }
            }
            else
            {
                var response = await _groupService.Update(groupModel, apiUrl);
                if (response != null)
                {
                    _navi.NavigateTo("/groups");
                }
            }
        }

        protected async Task AddStudentToGroup(Guid studentId)
        {
            var studentInGroup = new StudentInGroupDto
            {
                StudentId = studentId,
                GroupId = groupModel.Id
            };
            await _studentInGroupService.Add(studentInGroup, "api/studentInGroup");
            await refreshStudents();
        }

        protected async Task RemoveStudentFromGroup(Guid studentId)
        {
            await _studentInGroupService.Delete(studentId, "api/studentInGroup");
            await refreshStudents();
        }

        private async Task refreshStudents()
        {
            //getting all students that don't have a group but are in the same degree as that of the group in this page
            studentsWithoutGroup = await _studentService.GetAllById(groupModel.Generation.DegreeId, "api/studentInGroup/noGroup");
            //getting all students that are in the group
            studentsInGroup = await _studentService.GetAllById(groupModel.Id, "api/studentInGroup/byGroup");
        }
    }
}
