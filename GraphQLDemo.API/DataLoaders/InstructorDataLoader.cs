using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Services.Instructor;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLDemo.API.DataLoaders
{
    public class InstructorDataLoader : BatchDataLoader<int, InstructorDTO>
    {
        private readonly InstructorRepository _instructorRepository;
        public InstructorDataLoader(InstructorRepository instructorRepository,IBatchScheduler batchScheduler,DataLoaderOptions options=null):base(batchScheduler,options)
        {
            _instructorRepository = instructorRepository;
        }

        protected override async Task<IReadOnlyDictionary<int, InstructorDTO>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
             var instructors = await _instructorRepository.GetManyByIds(keys);

            return instructors.ToDictionary(x => x.Id);
        }
    }
}
