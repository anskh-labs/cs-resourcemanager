using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Helpers;
using NetCore.Security;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Settings;
using ResourceManager.ViewModels;
using ResourceManager.Views;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        private MainView mainWindow;
        private IDesignTimeDbContextFactory<ResourceManagerDBContext> ContextFactory => ServiceProvider.GetRequiredService<IDesignTimeDbContextFactory<ResourceManagerDBContext>>();
        private SystemIntegrationSettings SystemIntegrationSettings => ServiceProvider.GetRequiredService<IOptionsMonitor<SystemIntegrationSettings>>().CurrentValue;
        private IUiExecution UiExecution => ServiceProvider.GetRequiredService<IUiExecution>();
        private AppSplashScreen SplashScreen;
        public static AppSettings Settings => ServiceProvider.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue;
        public AppTrayIcon TrayIcon { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; } = null!;
        public App()
        {
            host = HostFactory.Create();
            ServiceProvider = host.Services;
        }
        private async Task StartApplication()
        {
            InitializeSpashScreen(true);

            await host.StartAsync();

            // background process
            await Task.Factory.StartNew(() =>
            {
                SplashScreen.Message = "Init database..";
                for (double i = 0; i <= 40; i = i + 5)
                {
                    SplashScreen.Progress = i;
                    Thread.Sleep(20);
                }
            });

            InitializeDatabase();

            await Task.Factory.StartNew(() =>
            {
                SplashScreen.Message = "Init configuration..";
                for (double i = 40; i <= 80; i = i + 5)
                {
                    SplashScreen.Progress = i;
                    Thread.Sleep(30);
                }
            });

            InitializePrincipal();
            InitializeConfiguration();

            await Task.Factory.StartNew(() =>
            {
                SplashScreen.Message = "Starting app..";
                for (double i = 80; i <= 100; i = i + 5)
                {
                    SplashScreen.Progress = i;
                    Thread.Sleep(30);
                }
            });

            InitializeMainWindow();
            InitializeTrayIcon();

            SplashScreen.IsVisible = false;

            if (SystemIntegrationSettings.ShowTrayIcon)
            {
                TrayIcon.IsVisible = true;
            }

            if (!SystemIntegrationSettings.ShowTrayIcon || !SystemIntegrationSettings.MinimizeToTrayOnStartup)
            {
                ShowMainWindow();
            }
        }

        private void InitializeConfiguration()
        {
            if (!Directory.Exists(Settings.EbookSettings.FolderPath))
                Directory.CreateDirectory(Settings.EbookSettings.FolderPath);
            if (!Directory.Exists(Settings.EbookSettings.CoverPath))
                Directory.CreateDirectory(Settings.EbookSettings.CoverPath);
            if (!Directory.Exists(Settings.ArticleSettings.FolderPath))
                Directory.CreateDirectory(Settings.ArticleSettings.FolderPath);
            if (!Directory.Exists(Settings.RepositorySettings.FolderPath))
                Directory.CreateDirectory(Settings.RepositorySettings.FolderPath);
        }

        private void InitializePrincipal()
        {
            //ensure set UserPrincipal
            AuthManager.SetCurrentPrincipal();
        }

        private void InitializeDatabase()
        {
            //ensure db created and migrated
            using (ResourceManagerDBContext context = ContextFactory.CreateDbContext(null!))
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
        private void InitializeSpashScreen(bool isSplashScreenVisible)
        {
            SplashScreen = new AppSplashScreen(Settings);
            SplashScreen.IsVisible = isSplashScreenVisible;
        }
        private void InitializeMainWindow()
        {
            UiExecution.Execute(() =>
            {
                var _viewLocator = ServiceProvider.GetRequiredService<ViewLocator>();
                mainWindow = (MainView)_viewLocator.GetViewForViewModel<MainViewModel>(ServiceProvider);
                mainWindow.Closing += MainWindowClosingEventHandler!;
                MainWindow = mainWindow;
            });
        }
        private void InitializeTrayIcon()
        {
            TrayIcon = new AppTrayIcon();
        }
        private void ShowMainWindow()
        {
            UiExecution.Execute(() =>
            {
                if (MainWindow.IsVisible)
                {
                    if (mainWindow.WindowState == WindowState.Minimized)
                    {
                        mainWindow.WindowState = WindowState.Normal;
                    }

                    MainWindow.Activate();
                }
                else
                {
                    MainWindow.Show();
                }
            });
        }
        private void MainWindowClosingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (TrayIcon.IsVisible)
            {
                e.Cancel = true;
                UiExecution.Execute(MainWindow.Hide);
            }
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            // To prevent opening of a second app instance
            if (!MutexLockHelper.CreateMutex())
            {
                Current.Shutdown();
                return;
            }

            base.OnStartup(e);

            await StartApplication();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync(TimeSpan.FromSeconds(5));

            host.Dispose();
        }
    }
}