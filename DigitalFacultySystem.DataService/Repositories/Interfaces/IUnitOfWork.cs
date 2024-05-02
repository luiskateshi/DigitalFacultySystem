﻿using DigitalFacultySystem.DataService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.DataService.Repositories.Interfaces
{
    public interface IUnitOfWork
    { 
        IStudentRepository Students { get; }
        IAcademicYearRepository AcademicYears { get; }
        IDepartmentRepository Departments { get; }
        IDegreeProgramRepository DegreePrograms { get; }
        ILecturerRepository Lecturers { get; }

        Task<bool> CompleteAsync();
    }
}
