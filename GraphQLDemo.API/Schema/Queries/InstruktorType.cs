using System;

namespace GraphQLDemo.API.Schema.Queries
{
    public class InstruktorType
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
    }
}
