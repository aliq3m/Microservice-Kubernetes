using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserService.Data;
using UserService.DTOs;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/users/{userid}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IUserRepo _repo;
        private IMapper _mapper;

        public PostsController(IUserRepo userRepo, IMapper mapper)
        {
            _repo = userRepo;
            _mapper = mapper;
        }
        //Get user posts
        //Get Post
        //Create Post

        [HttpGet]
        public ActionResult<IEnumerable<ReadPostDto>> GetUserPosts(int userId)
        {
            Console.WriteLine($" ==> Getting Posts for User Id: {userId}");

            if (!_repo.IsUserExist(userId))
            {
                return NotFound();
            }

            var posts = _repo.GetUserPosts(userId);

            return Ok(_mapper.Map<IEnumerable<ReadPostDto>>(posts));
        }

        [HttpGet("{postId}", Name = "GetPostForUser")]
        public ActionResult<ReadPostDto> GetPostForUser(int userId, int postId)
        {
            Console.WriteLine($"Getting one post for user with Id: {userId}, and Post ID: {postId}");
            var post = _repo.GetPost(userId, postId);
            if (post == null)
                return NotFound();

            return Ok(_mapper.Map<ReadPostDto>(post));
        }

        [HttpPost]
        public ActionResult<ReadPostDto> CreatePostForUser(int userId, CreatePostDto postDto)
        {
            Console.WriteLine($" ==> Hit CreatePostForUser with UserID: {userId}");

            if (!_repo.IsUserExist(userId))
                return NotFound();

            if (postDto == null)
                return NotFound();
            var post = _mapper.Map<Post>(postDto);
            _repo.CreatePost(userId, post);
            _repo.SaveChanges();

            var postReadDto = _mapper.Map<ReadPostDto>(post);

            return CreatedAtRoute(nameof(GetPostForUser),
                new { userId = userId, postId = postReadDto.Id }, postReadDto);
        }

        //[HttpPost]
        //public IActionResult TestInBoundConnection()
        //{
        //    Console.WriteLine("Inbound POST  *User service");
        //    return Ok("inbound test from POST controller");
        //}

    }
}
