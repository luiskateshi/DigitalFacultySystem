using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Client.Pages.Group
{
    public partial class Group
    {
        private AlertCard _alertCard;

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

        public string Message { get; set; }

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
            _alertCard.ShowAlert("Ka disa gabime validimi. Ju lutemi, provoni përsëri.", "alert-danger");
        }

        protected async Task HandleValidSubmit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                var response = await _groupService.Add(groupModel, apiUrl);
                if (response != null)
                {
                    _alertCard.ShowAlert("Grupi u shtua me sukses!", "alert-success");
                }
                else
                {
                    _alertCard.ShowAlert("Shtimi i grupit dështoi.", "alert-danger");
                }
            }
            else
            {
                var response = await _groupService.Update(groupModel, apiUrl);
                if (response != null)
                {
                    _alertCard.ShowAlert("Grupi u përditësua me sukses!", "alert-success");
                }
                else
                {
                    _alertCard.ShowAlert("Përditësimi i grupit dështoi.", "alert-danger");
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
            var response = await _studentInGroupService.Add(studentInGroup, "api/studentInGroup");
            if (response)
            {
                _alertCard.ShowAlert("Studenti u shtua në grup me sukses!", "alert-success");
            }
            else
            {
                _alertCard.ShowAlert("Shtimi i studentit në grup dështoi.", "alert-danger");
            }
            await refreshStudents();
        }

        protected async Task RemoveStudentFromGroup(Guid studentId)
        {
            var response = await _studentInGroupService.Delete(studentId, "api/studentInGroup");
            if (response)
            {
                _alertCard.ShowAlert("Studenti u hoq nga grupi me sukses!", "alert-success");
            }
            else
            {
                _alertCard.ShowAlert("Heqja e studentit nga grupi dështoi.", "alert-danger");
            }
            await refreshStudents();
        }

        private async Task refreshStudents()
        {
            if (groupModel.Generation != null)
            {
                // getting all students that don't have a group but are in the same degree as that of the group in this page
                studentsWithoutGroup = await _studentService.GetAllById(groupModel.Generation.DegreeId, "api/studentInGroup/noGroup");
                // getting all students that are in the group
                studentsInGroup = await _studentService.GetAllById(groupModel.Id, "api/studentInGroup/byGroup");
            }
        }
    }
}
