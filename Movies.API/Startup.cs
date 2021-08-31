using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Movies.API.MapperProfiles;
using Movies.API.Repositories;

namespace Movies.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movies.API", Version = "v1" });
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IEntertainerRepository, EntertainerRepository>();
            services.AddTransient<IUserMovieRepository, UserMovieRepository>();
            services.AddTransient<IMovieEntertainerRepository, MovieEntertainerRepository>();
            services.AddTransient<IEntertainerMovieRepository, EntertainerMovieRepository>();

            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(MovieProfile));
            services.AddAutoMapper(typeof(EntertainerProfile));
            services.AddAutoMapper(typeof(UserMovieProfile));
            services.AddAutoMapper(typeof(MovieEntertainerProfile));

            var region = Configuration["DynamoDBConfig:Region"];
            var serviceURL = Configuration["DynamoDBConfig:ServiceURL"];
            var config = new AmazonDynamoDBConfig();

            if (region != null) config.RegionEndpoint = RegionEndpoint.GetBySystemName(region);
            if (serviceURL != null) config.ServiceURL = serviceURL;

            var client = new AmazonDynamoDBClient(config);
            var db = new DynamoDBContext(client);
            
            services.AddSingleton<IDynamoDBContext>(db);            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
