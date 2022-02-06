using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.DTOs
{
    public class PostPublishedDto
    {
        public int id { get; set; }
        public string Title { get; set; }

        public string Event { get; set; }
    }
}
