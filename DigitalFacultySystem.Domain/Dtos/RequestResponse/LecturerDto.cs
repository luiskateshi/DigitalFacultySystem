using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    internal class LecturerDto
    {
        public Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateOnly? Birthdate { get; set; }
        public string? IdCard { get; set; }
        public string? Email { get; set; }
        public string? Tel { get; set; }
        public string? Grade { get; set; }
        public Guid? UserId { get; set; }
    }
}
