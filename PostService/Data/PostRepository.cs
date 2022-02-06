using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostService.Models;

namespace PostService.Data
{
    public class PostRepository:IPostRepository
    {
        private AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Post> GetAllPosts() => _context.Posts.OrderByDescending(p=>p.Id);


        public Post GetPostById(int id) => _context.Posts.FirstOrDefault(p => p.Id == id);


        public void CreatePost(Post post) => _context.Posts.Add(post);

    }
}
