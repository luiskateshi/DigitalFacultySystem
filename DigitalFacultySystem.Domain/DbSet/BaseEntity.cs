using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.DbSet
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool isActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

    }
}
