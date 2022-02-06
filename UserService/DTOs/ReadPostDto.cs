﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.DTOs
{
    public class ReadPostDto
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
