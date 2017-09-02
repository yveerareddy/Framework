using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;

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


    public class AsyncPubSubEvent<TPayload> : EventBase
    {

        private readonly IList<Tuple<SubscriptionToken, AsyncEventSubscription<TPayload>>> _subscriptions = new List<Tuple<SubscriptionToken, AsyncEventSubscription<TPayload>>>();

        public SubscriptionToken Subscribe(Action<TPayload> action)
        {
            return Subscribe(new AsyncEventSubscription<TPayload>(async x => await Task.Run(() => action(x))));
        }

        public SubscriptionToken Subscribe(Action<TPayload> action, Func<TPayload, bool> predicate)
        {
            return Subscribe(new AsyncEventSubscription<TPayload>(
                async x => await Task.Run(() => action(x)),
                async x => await Task.Run(() => predicate(x))));
        }

        public SubscriptionToken SubscribeAsync(Func<TPayload, Task> asyncFunc)
        {
            return Subscribe(new AsyncEventSubscription<TPayload>(asyncFunc));
        }

        public SubscriptionToken SubscribeAsync(Func<TPayload, Task> asyncFunc, Func<TPayload, bool> predicate)
        {
            return Subscribe(new AsyncEventSubscription<TPayload>(asyncFunc, async x => await Task.Run(() => predicate(x))));
        }

        public SubscriptionToken SubscribeAsync(Func<TPayload, Task> asyncFunc, Func<TPayload, Task<bool>> predicate)
        {
            return Subscribe(new AsyncEventSubscription<TPayload>(asyncFunc, predicate));
        }

        private SubscriptionToken Subscribe(AsyncEventSubscription<TPayload> subscription)
        {
            var token = new SubscriptionToken(UnsubscribeAsync);
            lock (_subscriptions)
            {
                _subscriptions.Add(Tuple.Create(token, subscription));
            }
            return token;

        }

        public virtual async Task PublishAsync(TPayload obj)
        {
            await PublishAsync(obj, AwaitOption.Synchronous);
        }

        public virtual async Task PublishAsync(TPayload obj, AwaitOption subscriberAwaitOption)
        {
            switch (subscriberAwaitOption)
            {
                case AwaitOption.Concurrent:
                    var tasks = _subscriptions.Select(async subscription =>
                    {
                        await InvokeSubscriber(subscription.Item2, obj);
                    });
                    await Task.WhenAll(tasks);
                    break;
                case AwaitOption.Synchronous:
                    foreach (var subscription in _subscriptions.ToList())
                    {
                        await InvokeSubscriber(subscription.Item2, obj);
                    }
                    break;
            }
        }

        private static async Task InvokeSubscriber(AsyncEventSubscription<TPayload> subscription, TPayload obj)
        {
            if (subscription.Predicate == null || await subscription.Predicate(obj))
            {
                await subscription.AsyncFunc(obj);
            }
        }

        private void UnsubscribeAsync(SubscriptionToken token)
        {
            lock (_subscriptions)
            {
                var subscription = _subscriptions.FirstOrDefault(x => x.Item1 == token);

                if (subscription != null)
                    _subscriptions.Remove(subscription);
            }
        }
    }

    public enum AwaitOption
    {
        Synchronous,
        Concurrent
    }


    public class AsyncEventSubscription<TPayload>
    {
        public Func<TPayload, Task> AsyncFunc { get; private set; }
        public Func<TPayload, Task<bool>> Predicate { get; private set; }

        public AsyncEventSubscription(Func<TPayload, Task> asyncFunc)
        {
            AsyncFunc = asyncFunc;
        }

        public AsyncEventSubscription(Func<TPayload, Task> asyncFunc, Func<TPayload, Task<bool>> predicate)
        {
            AsyncFunc = asyncFunc;
            Predicate = predicate;
        }
    }
}
