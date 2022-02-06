using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PostService.DTOs;

namespace PostService.SyncDataService.Http
{
    public class UserDataClient:IUserDataClient
    {
        private HttpClient _client;
        private IConfiguration _configuration;

        public UserDataClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task SendPostToUsers(PostReadDto postReadDto)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(postReadDto),
                Encoding.UTF8,
                "application/json"
                );

            var respons = await _client.PostAsync(_configuration["UserServiceUrl"], content);
            if (respons.IsSuccessStatusCode)
            {
                Console.WriteLine("==> sync POST TO user  service was ok!");
            }
            else
            {
                Console.WriteLine("==> sync POST TO user  service was Not ok!");
            }
        }
    }
}
