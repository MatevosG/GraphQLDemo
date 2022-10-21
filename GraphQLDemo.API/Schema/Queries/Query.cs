using Bogus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Queries
{
    public class Query
    {
        private readonly Faker<InstruktorType> _instruktorFaker;
        private readonly Faker<StudentType> _studentFaker;
        private readonly Faker<CourseType> _coursFaker;

        public Query()
        {
            _instruktorFaker = new Faker<InstruktorType>()
                               .RuleFor(c => c.Id, f => Guid.NewGuid())
                               .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                               .RuleFor(c => c.LastName, f => f.Name.LastName())
                               .RuleFor(c => c.Salary, f => f.Random.Double(0, 10000));


            _studentFaker = new Faker<StudentType>()
                            .RuleFor(c => c.Id, f => Guid.NewGuid())
                            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                            .RuleFor(c => c.LastName, f => f.Name.LastName())
                            .RuleFor(c => c.GPA, f => f.Random.Double(1, 4));


            _coursFaker = new Faker<CourseType>()
                           .RuleFor(c => c.Id, f => Guid.NewGuid())
                           .RuleFor(c => c.Name, f => f.Name.JobTitle())
                           .RuleFor(c => c.Students, f => _studentFaker.Generate(3))
                           .RuleFor(c => c.Instruktor, f => _instruktorFaker.Generate())
                .RuleFor(c => c.Subject, f => f.PickRandom<Subject>());

        }
        public IEnumerable<CourseType> GetCourses()
        {
            return _coursFaker.Generate(3);
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
