using Bogus;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema
{
    public class Query
    {
        private readonly Faker<CourseType> _coursFaker;


        public IEnumerable<CourseType> GetCourses()
        {
            Faker<CourseType> coursFaker = new Faker<CourseType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Name.JobTitle());
                //.RuleFor(c => c, f => Guid.NewGuid())
              //  .RuleFor(c => c.Id, f => Guid.NewGuid());

            var coures = coursFaker.Generate(5);

            return coures;
         
            //return new List<CourseType>()
            //{
            //    new CourseType
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "G"
            //    },
            //}
        }

        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            await Task.Delay(1000);

            CourseType course = _coursFaker.Generate();

            course.Id = id;

            return course;
        }


       // [GraphQLDeprecated("This query is deprecated")]
       // public string Instructions => null;
    }
}
