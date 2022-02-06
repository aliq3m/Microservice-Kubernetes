using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostService.Models;

namespace PostService.Data
{
   public interface IPostRepository
   {
       bool SaveChanges();
       IEnumerable<Post> GetAllPosts();
       Post GetPostById(int id);
       void CreatePost(Post post);
   }
}
