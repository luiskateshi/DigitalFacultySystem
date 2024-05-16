using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class ExamRetakeRequestDto
    {
        public Guid Id { get; set; }
        public Guid? StudentId { get; set; }
        public DateOnly? DateOfRequest { get; set; }
        public Guid? ExamId { get; set; }
        public String? ExamName { get; set; }
    }
}
