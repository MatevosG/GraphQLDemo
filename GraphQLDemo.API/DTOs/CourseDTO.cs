using GraphQLDemo.API.Models;
using System;
using System.Collections.Generic;

namespace GraphQLDemo.API.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public int  InstructorId { get; set; }
        public InstructorDTO  Instructor { get; set; }
        public IEnumerable<StudentDTO> Students { get; set; }
    }
}
