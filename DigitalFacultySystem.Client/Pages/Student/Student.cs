using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Client.Pages.Student
{
    public partial class Student
    {
        private AlertCard _alertCard;

        [Inject]
        public IGenericService<StudentDto> _studentService { get; set; }

        [Inject]
        public IGenericService<DegreeProgramDto> _degreeProgService { get; set; }

        public List<DegreeProgramDto> degreePrograms = new List<DegreeProgramDto>();

        [Parameter]
        public string studentId { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public StudentDto studentModel { get; set; } = new();

        private string url = "api/Student";

        protected override async Task OnInitializedAsync()
        {
            degreePrograms = await _degreeProgService.GetAll("api/DegreeProgram");
            if (!string.IsNullOrEmpty(studentId))
            {
                var Id = Guid.Parse(studentId);
                var student = await _studentService.GetById(Id, url);

                if (student != null)
                    studentModel = student;
                else
                    _alertCard.ShowAlert("Studenti nuk u gjet", "alert-danger");
            }
        }

        protected void HandleInValidSumbit()
        {
            _alertCard.ShowAlert("Ka disa gabime në validim. Ju lutemi provoni përsëri.", "alert-danger");
        }

        protected async Task HandleValidSumbit()
        {
            if (string.IsNullOrEmpty(studentId))
            {
                var newStudent = new StudentDto();
                MyFieldsMapper.MapFields(studentModel, newStudent);
                var result = await _studentService.Add(newStudent, url);
                if (result != null)
                {
                    _alertCard.ShowAlert("Studenti u shtua me sukses!", "alert-success");
                    _navi.NavigateTo("/students");
                }
                else
                {
                    _alertCard.ShowAlert("Shtimi i studentit dështoi.", "alert-danger");
                }
            }
            else
            {
                var updateStudent = new StudentDto();
                MyFieldsMapper.MapFields(studentModel, updateStudent);
                var result = await _studentService.Update(updateStudent, url);
                if (result)
                {
                    _alertCard.ShowAlert("Studenti u përditësua me sukses!", "alert-success");
                    _navi.NavigateTo("/students");
                }
                else
                {
                    _alertCard.ShowAlert("Përditësimi i studentit dështoi.", "alert-danger");
                }
            }
        }

        protected async Task DeleteStudent()
        {
            if (!string.IsNullOrEmpty(studentId))
            {
                var result = await _studentService.Delete(studentModel.Id, url);
                if (result)
                {
                    _alertCard.ShowAlert("Studenti u fshi me sukses!", "alert-success");
                    _navi.NavigateTo("/students");
                }
                else
                {
                    _alertCard.ShowAlert("Fshirja e studentit dështoi.", "alert-danger");
                }
            }
        }
    }
}
