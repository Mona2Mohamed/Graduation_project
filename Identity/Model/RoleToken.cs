﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Model
{
    public class RoleToken 
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
