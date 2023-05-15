using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using ResourceManager.Settings;
using ResourceManager.Views;
using System;

namespace ResourceManager
{
    public class AppSplashScreen : IDisposable
    {
        private string productName = string.Empty;
        private string productVersion = string.Empty;
        private SplashScreenWindow splashScreenWindow = null!;
        private IUiExecution UiExecution => App.ServiceProvider.GetRequiredService<IUiExecution>();
        public AppSplashScreen(AppSettings settings)
        {
            productName = settings.AppName;
            productVersion = settings.AppVersion;
        }
        public bool IsVisible
        {
            get => splashScreenWindow != null;
            set
            {
                if (value)
                {
                    InitializeSplashScreen();
                }
                else
                {
                    DisposeSplashScreen();
                }
            }
        }

        public string? Message
        {
            get => splashScreenWindow.Message;
            set => SetSplashScreenMessage(value ?? string.Empty);
        }
        public double? Progress
        {
            get => splashScreenWindow.Progress;
            set => SetSplashScreenProgress(value ?? 0);
        }

        private void SetSplashScreenProgress(double progress)
        {
            UiExecution.Execute(() => { splashScreenWindow.Progress = progress; });
        }

        private void InitializeSplashScreen()
        {

            UiExecution.Execute(() => {
                splashScreenWindow = new SplashScreenWindow();
                splashScreenWindow.productName.Text = productName;
                splashScreenWindow.productVersion.Text = productVersion;
                splashScreenWindow.message.Text = "Loading..";
                splashScreenWindow.Show();
            });
        }

        private void DisposeSplashScreen()
        {
            UiExecution.Execute(() => {
                if (splashScreenWindow == null)
                {
                    return;
                }

                splashScreenWindow.Close();
                splashScreenWindow = null!;
            });
        }
        private void SetSplashScreenMessage(string message)
        {
            UiExecution.Execute(() => { splashScreenWindow.Message = message; });
        }

        public void Dispose()
        {
            DisposeSplashScreen();
        }
    }
}
