using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateOnly? Birthdate { get; set; }
        public string? IdCard { get; set; }
        public string? Email { get; set; }
        public string? Tel { get; set; }
    }
}
