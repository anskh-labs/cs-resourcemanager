using NetCore.Mvvm.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace NetCore.Mvvm.Helpers
{
    public class UiExecution : IUiExecution
    {
        public void Execute(Action action, DispatcherPriority priority = DispatcherPriority.DataBind)
        {
            var dispatcher = Application.Current.Dispatcher ?? Dispatcher.CurrentDispatcher;
            dispatcher.Invoke(action, priority);
        }

        public Task ExecuteAsync(Action action, DispatcherPriority priority = DispatcherPriority.DataBind)
        {
            var dispatcher = Application.Current.Dispatcher ?? Dispatcher.CurrentDispatcher;
            return dispatcher.InvokeAsync(action, priority).Task;
        }
    }
}
