﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
