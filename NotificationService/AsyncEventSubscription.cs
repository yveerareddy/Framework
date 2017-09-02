using System;
using System.Threading.Tasks;

namespace NotificationService
{
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