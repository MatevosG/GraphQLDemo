using System;

namespace GraphQLDemo.API.Schema
{
    //public enum Subject
    //{
    //    Mathematics,
    //    Scienc,
    //    History
    //}

    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //Subject Subject { get; set; }
      //  public InstruktorType Instruktor { get; set; }  
       // public IEquatable<StudentType> Students { get; set; }
    }
}
