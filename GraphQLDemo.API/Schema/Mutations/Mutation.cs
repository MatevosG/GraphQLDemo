using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using HotChocolate;
using HotChocolate.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class Mutation
    {
       // private readonly List<CourseResult> _courses = new List<CourseResult>();

        private readonly CoursesRepository _coursesRepository ;

        public Mutation(List<CourseResult> courses, CoursesRepository coursesRepository)
        {
            
            _coursesRepository = coursesRepository;
        }

        public async Task<CourseResult> CreateCourse(CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
        {

            CourseDTO courseDTO = new CourseDTO
            {
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstructorId = courseInputType.InstruktorId
            };

            _coursesRepository.Create(courseDTO);

            CourseResult course   = new CourseResult
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstruktorId = courseDTO.InstructorId
            };
           // _coursesRepository.Create(course);
            
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated) , course);
            return course;
        }

        public async Task<CourseResult> UpdateCourse(Guid id,CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
        {
            var course = _courses.FirstOrDefault(x => x.Id == id);

            if (course == null)
            {
                throw new Exception("Coure not found");
            }

            course.Name = courseInputType.Name; 
            course.Subject = courseInputType.Subject;   
            course.InstruktorId = courseInputType.InstruktorId;

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";

            await topicEventSender.SendAsync(updateCourseTopic, course);
         
            return course;
        }


        public bool DeleteCourse(Guid id)
        {
            var course = _courses.FirstOrDefault(x=>x.Id == id);
            if (course == null)
            {
                throw new GraphQLException(new Error("Course not found.", "havest chka"));
            }
            _courses.Remove(course);
            return true;
        }
    }
}
