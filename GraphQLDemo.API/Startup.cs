using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Services.Instructor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GraphQLDemo.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionstring = _configuration.GetConnectionString("default");

            services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlServer(connectionstring).LogTo(Console.WriteLine));
            services.AddGraphQLServer().RegisterDbContext<SchoolDbContext>()
                                      .AddQueryType<Query>()
                                      .AddMutationType<Mutation>()
                                      .AddSubscriptionType<Subscription>()
                                      .AddType<CourseType>()
                                      //.AddType<CourseDTO>()
                                      .AddType<InstruktorType>()
                                      .AddType<StudentType>()
                                      .AddFiltering();

            services.AddInMemorySubscriptions();

            //services.AddDbContext<SchoolDbContext>(options =>
            //                                       options.UseSqlServer(_configuration.GetConnectionString("default")));

            //services.AddScoped<ICoursesRepository,CoursesRepository> ();


            //string connectionstring = _configuration.GetConnectionString("default");

            

           
            //services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlServer(connectionstring).LogTo(Console.WriteLine));
           // services.AddDbContext<SchoolDbContext>(o => o.UseSqlServer(connectionstring).LogTo(Console.WriteLine));

            services.AddScoped<CoursesRepository>(); 
            services.AddScoped<InstructorRepository>();
            services.AddScoped<InstructorDataLoader>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
