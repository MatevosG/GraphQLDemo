using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Services.Instructor
{
    public class InstructorRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;
        //private readonly SchoolDbContext _contextFactory;

        public InstructorRepository(IDbContextFactory<SchoolDbContext> context)
        {
            _contextFactory = context;
        }

        public async Task<IEnumerable<InstructorDTO>> GetAllInstructors()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Instructors.ToListAsync();
            }
        }
        public async Task<InstructorDTO> GetInstructorById(int id)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Instructors.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<IEnumerable<InstructorDTO>>  GetManyByIds(IReadOnlyList<int> instructorIds)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Instructors.Where(i => instructorIds.Contains(i.Id)).ToListAsync();
            }
        }
    }
}
