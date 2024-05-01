using DigitalFacultySystem.ClientApp.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.Requests;
using DigitalFacultySystem.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Components;
using System.Dynamic;

namespace DigitalFacultySystem.ClientApp.Pages.Student
{
    public partial class Student
    {
        [Inject]
        public IStudentService _studentService { get; set; } 

        [Parameter] 
        public String studentId { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        public StudentResponse studentModel { get; set; } = new ();

        public String Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrEmpty(studentId))
            {
                var Id = Guid.Parse(studentId);
                var student = await _studentService.GetStudent(Id);

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
                var newStudent = new CreateStudentRequest()
                {
                    Firstname = studentModel.Firstname,
                    Lastname = studentModel.Lastname,
                    Email = studentModel.Email,
                    Birthdate = studentModel.Birthdate,
                    IdCard = studentModel.IdCard,
                    Tel = studentModel.Tel
                };
                var result = await _studentService.AddStudent(newStudent);
                if(result != null)
                    _navi.NavigateTo("/students");

                Message = "Failed to add student";

            }
            else
            {
                var updateStudent = new UpdateStudentRequest()
                {
                    Id = studentModel.Id,
                    Firstname = studentModel.Firstname,
                    Lastname = studentModel.Lastname,
                    Email = studentModel.Email,
                    Birthdate = studentModel.Birthdate,
                    IdCard = studentModel.IdCard,
                    Tel = studentModel.Tel
                };  
                var result = await _studentService.UpdateStudent(updateStudent);
                if(result)
                    _navi.NavigateTo("/students");
                
                Message = "Failed to update student";
            }
        }

        protected async void DeleteStudent()
        {
            if(!string.IsNullOrEmpty(studentId))
            {
                var result = await _studentService.DeleteStudent(studentModel.Id);
                if(result)
                    _navi.NavigateTo("/students");
                Message = "Failed to delete student";
            }
            
        }
    }
}
