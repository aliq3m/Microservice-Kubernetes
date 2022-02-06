using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostService.Models;

namespace PostService.Data
{
    public static class SeedDb
    {
        public static void Seed(IApplicationBuilder app,bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedDate(serviceScope.ServiceProvider.GetService<AppDbContext>(),isProduction);
            }

        }

        private static void SeedDate(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("=====> attempting to apply migration");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine("=====> migration is failed");
                }

            }



            if ((!context.Posts.Any()))
            {
                Console.WriteLine("seeding data=====>");
                context.Posts.AddRange(
                
                    new Post()
                    {
                        Title = "title1",
                        Content = "content1",
                        PublishDate = DateTime.Now,
                        
                    },
                    new Post()
                    {
                        Title = "title2",
                        Content = "content2",
                        PublishDate = DateTime.Now.AddHours(-10),

                    }, new Post()
                    {
                        Title = "title3",
                        Content = "content3",
                        PublishDate = DateTime.Now.AddHours(-12),

                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
