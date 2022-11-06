using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Services.Courses
{
    public class CoursesRepository//: ICoursesRepository
    {
       private readonly IDbContextFactory<SchoolDbContext> _contextFactory;
       

        public CoursesRepository(IDbContextFactory<SchoolDbContext> context)
        {
            _contextFactory = context;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCouses()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Courses.ToListAsync();
            }
        }
        public  IQueryable<CourseDTO> GetAllQUeryCouses()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Courses.AsQueryable();
            }
        }

        public async Task<CourseDTO> GetCourseById(int id)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public  CourseDTO Create(CourseDTO course)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Courses.Add(course);
                context.SaveChanges();
                return course;
            }
        }

        public async Task<CourseDTO> Update(CourseDTO course)
        {

            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Courses.Update(course);
                await context.SaveChangesAsync();
                return course;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                CourseDTO course = new CourseDTO()
                {
                    Id = id
                };
                context.Courses.Remove(course);

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
