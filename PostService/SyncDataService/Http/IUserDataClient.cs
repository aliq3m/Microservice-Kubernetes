using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostService.DTOs;

namespace PostService.SyncDataService.Http
{
   public interface IUserDataClient
    {
        Task SendPostToUsers(PostReadDto postReadDto);
    }
}
