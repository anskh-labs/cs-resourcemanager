using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.Helpers;
using NetCore.Mvvm.ViewModels;
using System.Linq;
using System.Windows;

namespace NetCore.Mvvm.Extensions
{
    /// <summary>
    /// IServiceCollection extension methods for common scenarios.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds required services to the given service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddMvvm(this IServiceCollection services)
        {
            services.AddSingleton<ViewLocator>();
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IPopupManager, PopupManager>();
            services.AddSingleton<IUiExecution, UiExecution>();
            services.AddMvvmSingleton<PopupMessageViewModel, PopupMessageView>();
            return services;
        }

        /// <summary>
        /// Adds a transient viewmodel and its corresponding view to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the types to.</param>
        /// <typeparam name="TViewModel">The type of the viewmodel to add.</typeparam>
        /// <typeparam name="TView">The type of the view to add.</typeparam>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddMvvmTransient<TViewModel, TView>(this IServiceCollection services) where TViewModel : class where TView : FrameworkElement
        {
            services
                .AddTransient<TViewModel>()
                .AddTransient<TView>();

            services
                .GetViewModelRegistry()
                .ViewModelTypeToViewType.Add(typeof(TViewModel), typeof(TView));

            return services;
        }

        /// <summary>
        /// Adds a singleton viewmodel and its corresponding view to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the types to.</param>
        /// <typeparam name="TViewModel">The type of the viewmodel to add.</typeparam>
        /// <typeparam name="TView">The type of the view to add.</typeparam>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddMvvmSingleton<TViewModel, TView>(this IServiceCollection services) where TViewModel : class where TView : FrameworkElement
        {
            services
                .AddSingleton<TViewModel>()
                .AddTransient<TView>();

            services
                .GetViewModelRegistry()
                .ViewModelTypeToViewType.Add(typeof(TViewModel), typeof(TView));

            return services;
        }

        private static ViewModelRegistry GetViewModelRegistry(this IServiceCollection services)
        {
            ViewModelRegistry registry;
            var descriptor = services.FirstOrDefault(x => x.ServiceType == typeof(ViewModelRegistry));
            if (descriptor != null)
            {
                registry = (ViewModelRegistry)descriptor.ImplementationInstance!;
            }
            else
            {
                registry = new ViewModelRegistry();
                services.AddSingleton(registry);
            }

            return registry;
        }
    }
}
