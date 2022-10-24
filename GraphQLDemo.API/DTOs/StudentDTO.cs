using System;

namespace GraphQLDemo.API.DTOs
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GPA { get; set; }
    }
}
