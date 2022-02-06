using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.DTOs
{
    public class CreatePostDto
    {
        
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
       
    }
}
