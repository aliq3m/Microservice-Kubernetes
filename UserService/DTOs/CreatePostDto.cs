﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.DTOs
{
    public class CreatePostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
