using GraphQLDemo.API.Schema.Queries;
using System;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class CourseResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject  Subject { get; set; }
        public Guid  InstruktorId { get; set; }
    }
}
