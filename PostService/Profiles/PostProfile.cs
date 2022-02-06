using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Profiles
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostReadDto>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<PostReadDto, PostPublishedDto>() ;
        }
    }
}
