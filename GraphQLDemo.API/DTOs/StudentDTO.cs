using System;
using System.Collections.Generic;

namespace GraphQLDemo.API.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GPA { get; set; }
        public IEnumerable<CourseDTO> Courses { get; set; }
    }
}
