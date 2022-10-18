using System;

namespace GraphQLDemo.API.Schema
{
    public class InstruktorType
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
    }
}
