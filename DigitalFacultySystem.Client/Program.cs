using Blazored.LocalStorage;
using DigitalFacultySystem.Client.Authentication;
using DigitalFacultySystem.Client.Services;
using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.DataService.Repositories.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DigitalFacultySystem.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddScoped<IGenericService<DepartmentDto>, GenericService<DepartmentDto>>();
            builder.Services.AddScoped<IGenericService<AcademicYearDto>, GenericService<AcademicYearDto>>();
            builder.Services.AddScoped<IGenericService<StudentDto>, GenericService<StudentDto>>();
            builder.Services.AddScoped<IGenericService<DegreeProgramDto>, GenericService<DegreeProgramDto>>();
            builder.Services.AddScoped<IGenericService<LecturerDto>, GenericService<LecturerDto>>();
            builder.Services.AddScoped<IGenericService<StudyPlanDto>, GenericService<StudyPlanDto>>();
            builder.Services.AddScoped<IGenericService<SubjectDto>, GenericService<SubjectDto>>();
            builder.Services.AddScoped<IGenericService<GenerationDto>, GenericService<GenerationDto>>();
            builder.Services.AddScoped<IGenericService<GroupDto>, GenericService<GroupDto>>();
            builder.Services.AddScoped<IGenericService<CourseDto>, GenericService<CourseDto>>();
            builder.Services.AddScoped<IGenericService<ExamSessionDto>, GenericService<ExamSessionDto>>();
            builder.Services.AddScoped<IGenericService<StudentInGroupDto>, GenericService<StudentInGroupDto>>();
            builder.Services.AddScoped<IGenericService<StudyPlanSubjectDto>, GenericService<StudyPlanSubjectDto>>();
            builder.Services.AddScoped<IGenericService<CourseAttendanceDto>, GenericService<CourseAttendanceDto>>();
            builder.Services.AddScoped<IGenericService<ExamDto>, GenericService<ExamDto>>();
            builder.Services.AddScoped<IGenericService<StudentsInExamDto>, GenericService<StudentsInExamDto>>();
            builder.Services.AddScoped<IGenericService<ExamRetakeRequestDto>, GenericService<ExamRetakeRequestDto>>();
            builder.Services.AddScoped<IGenericService<PossibleExamRetakesDto>, GenericService<PossibleExamRetakesDto>>();
            builder.Services.AddScoped<IGenericService<StudentExamGradesDto>, GenericService<StudentExamGradesDto>>();


            builder.Services.AddScoped(hc => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5010/")
            });

            //authentication & authorization
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddScoped<IUserAccount, AccountService>();


            await builder.Build().RunAsync();
        }
    }
}
