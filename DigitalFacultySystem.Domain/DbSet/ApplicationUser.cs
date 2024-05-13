using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DigitalFacultySystem.Entities.DbSet
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
