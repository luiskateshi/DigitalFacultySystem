using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components;
using System.Dynamic;

namespace DigitalFacultySystem.ClientApp.Pages.Student
{
    public partial class Student
    {
        [Inject]
        public IGenericService<StudentDto> _studentService { get; set; }

        [Inject]
        public IGenericService<DegreeProgramDto> _degreeProgService { get; set; }

        public List<DegreeProgramDto> degreePrograms = new List<DegreeProgramDto>();

        [Parameter] 
        public String studentId { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public StudentDto studentModel { get; set; } = new ();

        public String Message { get; set; } = string.Empty;

        private string url = "api/Student";

        protected override async Task OnInitializedAsync()
        {
            degreePrograms = await _degreeProgService.GetAll("api/DegreeProgram");
            if(!string.IsNullOrEmpty(studentId))
            {
                var Id = Guid.Parse(studentId);
                var student = await _studentService.GetById(Id, url);

                if(student != null)
                    studentModel = student;
            }
        }

        protected void HandleInValidSumbit()
        {
            Message = "There are validation errors. Please try again.";
        }

        protected async Task HandleValidSumbit()
        {
            if(string.IsNullOrEmpty(studentId))
            {
                var newStudent = new StudentDto();
                MyFieldsMapper.MapFields(studentModel, newStudent);
                var result = await _studentService.Add(newStudent, url);
                if(result != null)
                    _navi.NavigateTo("/students");

                Message = "Failed to add student";

            }
            else
            {
                var updateStudent = new StudentDto();
                MyFieldsMapper.MapFields(studentModel, updateStudent);
                var result = await _studentService.Update(updateStudent, url);
                if(result)
                    _navi.NavigateTo("/students");
                
                Message = "Failed to update student";
            }
        }

        protected async void DeleteStudent()
        {
            if(!string.IsNullOrEmpty(studentId))
            {
                var result = await _studentService.Delete(studentModel.Id, url);
                if(result)
                    _navi.NavigateTo("/students");
                Message = "Failed to delete student";
            }
            
        }
    }
}
