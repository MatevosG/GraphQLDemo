using System;
using GraphQLDemo.API.Models;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class CourseInputType
    {
        public string Name { get; set; }    
        public Subject Subject { get; set; }
        public Guid InstruktorId { get; set; }
    }
}
