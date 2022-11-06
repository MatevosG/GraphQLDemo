using GraphQLDemo.API.Models;
using System;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class CourseResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Subject  Subject { get; set; }
        public int  InstruktorId { get; set; }
    }
}
