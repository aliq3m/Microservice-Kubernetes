using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Data
{
   public interface IUserRepo
    {
        bool SaveChanges();

        //Posts
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> GetUserPosts(int userId);
        Post GetPost(int userId, int postId);
        void CreatePost(int userId, Post post);
        bool IsPostExist(int postId);

        bool ExternalPostExists(int externalPostId);

        //Users
        IEnumerable<User> GetAllUsers();
        bool IsUserExist(int userId);
        User GetUserById(int id);
        void CreateUser(User user);
    }
}
