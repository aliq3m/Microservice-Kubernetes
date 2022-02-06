using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostService.AsyncDataService;
using PostService.Data;
using PostService.SyncDataService.Http;

namespace PostService
{
    public class Startup
    {
       
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get;  }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostService", Version = "v1" });
            });


            #region Db

            if (_env.IsProduction())
            {
                Console.WriteLine(" ==> Using SQL Server Db Provider.");
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("PostserviceConnection")));
            }
            else
            {
                services.AddDbContext<AppDbContext>(option =>
                {
                    Console.WriteLine(" ==> Using In Memory Db Provider.");

                    option.UseInMemoryDatabase("PostsDb");
                });

            }

            #endregion

            #region IOC

            services.AddHttpClient<IUserDataClient, UserDataClient>();
            services.AddSingleton<IMessageBusClient,MessageBusClient>();
            services.AddScoped<IPostRepository, PostRepository>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PostService v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            SeedDb.Seed(app, env.IsProduction());
        }
    }
}
