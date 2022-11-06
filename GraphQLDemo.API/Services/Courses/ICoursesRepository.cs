using GraphQLDemo.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Services.Courses
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<CourseDTO>> GetAllCouses();
        Task<CourseDTO> GetCourseById(int id);
        CourseDTO Create(CourseDTO course);
        Task<CourseDTO> Update(CourseDTO course);
        Task<bool> Delete(int id);
    }
}
