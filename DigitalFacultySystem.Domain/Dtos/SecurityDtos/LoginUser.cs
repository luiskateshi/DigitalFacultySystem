﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFacultySystem.Entities.Dtos.SecurityDtos
{
    public record LoginUser
    {
        [EmailAddress, Required]
        public string? Email { get; set; }
        [DataType(DataType.Password), Required]
        public string? Password { get; set; }
    }
}
