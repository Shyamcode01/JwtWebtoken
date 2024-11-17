
namespace Mongo.MessageBus
{
    public class MessageBus : IMessageBus
    {
        public Task PublishMessage(object message, string topic_queue_name)
        {
            throw new NotImplementedException();
        }
    }
}
