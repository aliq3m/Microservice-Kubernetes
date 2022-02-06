using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserService.DTOs;
using UserService.Models;

namespace UserService.Profiles
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Post, ReadPostDto>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<CreatePostDto, User>();
            CreateMap<PostPublishedDto, Post>()
                .ForMember(dest => dest.ExtenralId, opt =>
                    opt.MapFrom(p => p.id));
        }
    }
}
