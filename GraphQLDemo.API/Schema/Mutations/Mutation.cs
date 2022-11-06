using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Courses;
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
        private readonly CoursesRepository _coursesRepository;

        public Mutation(List<CourseResult> courses, CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public CourseResult CreateCourse(CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
        {

            CourseDTO courseDTO = new CourseDTO
            {
                //Id = courseInputType.Id,
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstructorId = courseInputType.InstruktorId
            };

            _coursesRepository.Create(courseDTO);

            CourseResult course = new CourseResult
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstruktorId = courseDTO.InstructorId
            };
            // _coursesRepository.Create(course);

            topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
            return course;
        }

        public async Task<CourseResult> UpdateCourse(CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
        {
            CourseDTO courseDTO = new CourseDTO
            {
                Id = courseInputType.Id,
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstructorId = courseInputType.InstruktorId
            };
            courseDTO = await _coursesRepository.Update(courseDTO);

            CourseResult course = new CourseResult
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstruktorId = courseDTO.InstructorId
            };

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";

            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            try
            {
                await _coursesRepository.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
