using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Queries
{
    public class Query
    {
        private readonly  CoursesRepository _coursesRepository;
        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            IEnumerable<CourseDTO> coursedtoss = await _coursesRepository.GetAllCouses();

            return coursedtoss.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
            });
        }


        [UseDbContext(typeof(SchoolDbContext))]
        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseFiltering]
        public IQueryable<CourseType> GetPaginatedCourses([ScopedService] SchoolDbContext DbContext)
        {

           // IQueryable<CourseDTO> coursedtoss = _coursesRepository.GetAllQUeryCouses();
            // IEnumerable<CourseDTO> coursedtoss =  _coursesRepository.GetAllQUeryCouses();

            return DbContext.Courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
            });
        }

        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        public async Task<IEnumerable<CourseType>> GetOffsetCourses()
        {
            IEnumerable<CourseDTO> coursedtoss = await _coursesRepository.GetAllCouses();

            //var coursedtos = _context.Courses.AsEnumerable();

            return coursedtoss.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
            });
        }

        public async Task<CourseType> GetCourseById(int id)
        {
            CourseDTO coursedto = await _coursesRepository.GetCourseById(id);

            return new CourseType()
            {
                Id = coursedto.Id,
                Name = coursedto.Name,
                Subject = coursedto.Subject,
                InstructorId = coursedto.InstructorId,
            };
        }


        [GraphQLDeprecated("This query is deprecated")]
        public string Instructions => "lllllllllllllllllllllllllll";
    }
}
