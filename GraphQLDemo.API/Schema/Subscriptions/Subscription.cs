using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid coursId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{coursId}_{nameof(Subscription.CourseCreated)}";

            return topicEventReceiver.SubscribeAsync<string,CourseResult>(topicName);
        }
    }
}
