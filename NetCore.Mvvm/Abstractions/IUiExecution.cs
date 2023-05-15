using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace NetCore.Mvvm.Abstractions
{
    /// <summary>
    /// Provides an method to execute an action in the dispatcher thread.
    /// </summary>
    public interface IUiExecution
    {
        /// <summary>
        /// Executes the passed action in the dispatcher thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="priority">Dispatcher priority</param>
        void Execute(Action action, DispatcherPriority priority = DispatcherPriority.DataBind);

        /// <summary>
        /// Executes the passed action in the dispatcher thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// /// <param name="priority">Dispatcher priority</param>
        Task ExecuteAsync(Action action, DispatcherPriority priority = DispatcherPriority.DataBind);
    }
}
