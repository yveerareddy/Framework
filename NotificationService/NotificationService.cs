using System;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService
{
    public interface ISubscriptionService
    {
        Task SubscribeAsync<T,TResponse>(Topic topic,Func<T,Task<TResponse>> callbackAsync );
        Task UnSubscribeAsync(Topic topic);
    }
    
    public interface IPublishService
    {
        Task PublishAsync<T>(Topic topic, T payload) where T:Payload;
    }

    public class Payload
    {
    }

    public class Topic
    {
    }


    public enum AwaitOption
    {
        Synchronous,
        Concurrent
    }
}
