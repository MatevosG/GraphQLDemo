using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Models;
using GraphQLDemo.API.Services.Instructor;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Queries
{
    public class CourseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstructorId { get; set; }

        public Subject Subject { get; set; }
        [GraphQLNonNullType]
        public async Task<InstruktorType> Instruktor([Service] InstructorRepository instructorRepository)
        {
            InstructorDTO instructorDto  = await instructorRepository.GetInstructorById(InstructorId);
            return new InstruktorType
            {
                Id = instructorDto.Id,
                FirstName = instructorDto.FirstName,
                LastName = instructorDto.LastName,
                Salary = instructorDto.Salary,
            };
        }
        public IEnumerable<StudentType> Students { get; set; }
    }
}
