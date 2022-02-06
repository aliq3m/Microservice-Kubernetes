using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PostService.AsyncDataService;
using PostService.Data;
using PostService.DTOs;
using PostService.Models;
using PostService.SyncDataService.Http;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostRepository _repository;
        private IMapper _mapper;
        private IUserDataClient _client;
        private IMessageBusClient _busClient;

        public PostController(IPostRepository repository, IMapper mapper, IUserDataClient client, IMessageBusClient busClient)
        {
            _repository = repository;
            _mapper = mapper;
            _client = client;
            _busClient = busClient;
        }
       
        [HttpGet]
        public ActionResult<IEnumerable<PostReadDto>> GetAllPosts()
        {
            var posts = _repository.GetAllPosts();
            var postReadItems = _mapper.Map<IEnumerable<PostReadDto>>(posts);
            return Ok(postReadItems);
        }
        [HttpGet("id",Name = "GetPostById")]
        public ActionResult<PostReadDto> GetPostById(int id)
        {
            var post = _repository.GetPostById(id);
            if (post==null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PostReadDto>(post));
        }
        [HttpPost]
        public async Task<ActionResult<PostReadDto>> CreatePost(CreatePostDto post)
        {
            var p = _mapper.Map<Post>(post);
            p.PublishDate = DateTime.Now;
            _repository.CreatePost(p);
            _repository.SaveChanges();
            var postRead = _mapper.Map<PostReadDto>(p);
            //Sync
            try
            {
              await  _client.SendPostToUsers(postRead);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //Async
            try
            {
                var postPublishedDto = _mapper.Map<PostPublishedDto>(postRead);
                postPublishedDto.Event = "Post_Published";
                _busClient.PublishNewPost(postPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ==> Could not send message, {ex.Message}");
            }


            return CreatedAtRoute(nameof(GetPostById), new {id = postRead.Id}, postRead);
        }
    }
}
