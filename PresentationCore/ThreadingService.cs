using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WPFPresentationCore
{
    [Export(typeof(IThreadingService))]
    public sealed class ThreadingService:IThreadingService
    {
        private Dispatcher _dispatcher;

        //public Dispatcher Dispatcher { get; set; }

        public ThreadingService(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void BeginInvokeInUI(Action action)
        {
            _dispatcher.BeginInvoke(action);
        }

        public void InvokeInUI(Action action)
        {
            _dispatcher.Invoke(action);
        }

        public Task InvokeInUIAsync(Action action)
        {
            return  _dispatcher.InvokeAsync(action).Task;
        }

        public Task InvokeInUIAsync(Func<Task> action)
        {
            return _dispatcher.InvokeAsync(action).Task;
        }

        public Task StartNewTask(Action action)
        {
            return Task.Factory.StartNew(action);
        }
    }

    public interface IThreadingService
    {
        void BeginInvokeInUI(Action action);
        void InvokeInUI(Action action);
        Task InvokeInUIAsync(Func<Task> action);
        Task InvokeInUIAsync(Action action);
        Task StartNewTask(Action action);

    }


}
