using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using System;
using System.Text;
using System.Windows;

namespace ResourceManager
{
    public class AppTrayIcon : IDisposable
    {
        private System.Windows.Forms.NotifyIcon trayIcon = null!;
        private IUiExecution UiExecution => App.ServiceProvider.GetRequiredService<IUiExecution>();
        protected IWindowManager WindowManager => App.ServiceProvider.GetRequiredService<IWindowManager>();
        public bool IsVisible
        {
            get => trayIcon != null && trayIcon.Visible;
            set
            {
                if (value)
                {
                    InitializeTrayIcon();
                }
                else
                {
                    DisposeTrayIcon();
                }
            }
        }

        private void InitializeTrayIcon()
        {
            UiExecution.Execute(() => {
                trayIcon = new System.Windows.Forms.NotifyIcon();
                trayIcon.Text = "Resource Manager";
                trayIcon.Icon = Resources.Resources.TrayIcon;
                trayIcon.ContextMenuStrip = BuildTrayContextMenu();
                trayIcon.DoubleClick += (sender, args) => ShowApplicationMainWindow();
                trayIcon.Visible = true;
            });
        }

        private System.Windows.Forms.ContextMenuStrip BuildTrayContextMenu()
        {
            var contextMenu = new System.Windows.Forms.ContextMenuStrip();

            contextMenu.Items.Add("Show Application",
                null,
                (sender, args) => ShowApplicationMainWindow()
            );

            contextMenu.Items.Add(new System.Windows.Forms.ToolStripSeparator());

            contextMenu.Items.Add("Exit",
                null,
                (sender, args) => ExitApplication()
            );

            return contextMenu;
        }

        private void ShowApplicationMainWindow()
        {
            UiExecution.Execute(() =>
            {
                var mainWindow = Application.Current.MainWindow;
                if (mainWindow == null)
                {
                    return;
                }

                if (mainWindow.IsVisible)
                {
                    if (mainWindow.WindowState == WindowState.Minimized)
                    {
                        mainWindow.WindowState = WindowState.Normal;
                    }

                    mainWindow.Activate();
                }
                else
                {
                    mainWindow.Show();
                }
            });
        }

        private void ExitApplication()
        {
            var result = WindowManager.ShowMessageBox("Close application?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result== MessageBoxResult.Yes)
                Application.Current.Shutdown();
        
        }

        private void DisposeTrayIcon()
        {
            UiExecution.Execute(() => {
                if (trayIcon == null)
                {
                    return;
                }

                trayIcon.Visible = false;
                trayIcon = null!;
            });
        }

        public void Dispose()
        {
            DisposeTrayIcon();
        }
    }
}
