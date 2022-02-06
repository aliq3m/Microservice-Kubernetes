using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostService.DTOs;

namespace PostService.AsyncDataService
{
   public interface IMessageBusClient
   {
       void PublishNewPost(PostPublishedDto post);
   }
}
