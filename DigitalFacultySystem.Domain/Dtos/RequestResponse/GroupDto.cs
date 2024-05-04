using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.RequestResponse
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? GenerationId { get; set; }
    }
}
