using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class StudentInGroupDto
    {
        public Guid Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? GroupId { get; set; }
        public bool isActive { get; set; }
    }
}
