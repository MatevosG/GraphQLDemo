using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddGraphQLServer()
                                      .AddQueryType<Query>()
                                      .AddMutationType<Mutation>()
                                      .AddSubscriptionType<Subscription>()
                                      .AddType<CourseType>()
                                      //.AddType<CourseResult>()
                                      .AddType<InstruktorType>()
                                      .AddType<StudentType>();

            services.AddInMemorySubscriptions();

            string connectionstring = _configuration.GetConnectionString("default");

            services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlite(connectionstring));

            services.AddScoped<CoursesRepository>();

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
