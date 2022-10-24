using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
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
        private readonly List<CourseResult> _courses = new List<CourseResult>();
        public async Task<CourseResult> CreateCourse(CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult course   = new CourseResult
            {
                Id = Guid.NewGuid(),
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstruktorId = courseInputType.InstruktorId
            };
            _courses.Add(course);
            
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
