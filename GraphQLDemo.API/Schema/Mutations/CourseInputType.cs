using System;
using GraphQLDemo.API.Models;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class CourseInputType
    {
        public int Id { get; set; }

        public string Name { get; set; }    
        public Subject Subject { get; set; }
        public int InstruktorId { get; set; }
    }
}
